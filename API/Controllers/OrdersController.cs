using API.BusinessLayer;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Controllers/OrdersController.cs
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //  [Authorize] // require JWT
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;


        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userId = User.Identity?.Name; // or claim "sub"/"username"
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
           try
            {
                var order = await _orderService.CreateOrderAsync(userId, dto);
                if (order == null)
                {
                    _logger.LogWarning("Order {OrderId} not found for user {UserId}", userId);
                    return NotFound();
                }

                return Ok(MapToResponse(order));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order {OrderId} for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            userId= "admin";
            var order = await _orderService.GetOrderAsync(id, userId);
            if (order == null) return NotFound();

            return Ok(MapToResponse(order));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetMyOrdersv1([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            //var userId = User.Identity?.Name;
            //if (string.IsNullOrEmpty(userId))
            //    return Unauthorized();
            string userId = "admin";
            var orders = await _orderService.GetUserOrdersAsync(userId.ToString());
            var firstOrder = orders.FirstOrDefault(o => o.Id == 6);
            return Ok(MapToOrderDto(firstOrder));
        }


        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetMyOrdersv2([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            string userId = "admin";
            var result = await _orderService.GetPagedOrdersAsync(userId, pageNumber, pageSize);
            return Ok(result);
        }

        private static OrderResponseDto MapToOrderDto(Order order)
        {
            if (order == null)
                return null;

            return new OrderResponseDto
            {
                Id = order.Id,
                //UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }
        private static OrderResponseDto MapToResponse(Order order) =>
            new()
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
    }
}
