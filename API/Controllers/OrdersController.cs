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
            string userId = "admin";

            _logger.LogInformation("GetMyOrdersv1 called for user {UserId} with pageNumber={PageNumber}, pageSize={PageSize}",
                userId, pageNumber, pageSize);

            try
            {
                var orders = await _orderService.GetUserOrdersAsync(userId);

                if (orders == null || !orders.Any())
                {
                    _logger.LogWarning("No orders found for user {UserId}", userId);
                    return NotFound("No orders found");
                }

               if (orders == null)
                {
                    _logger.LogWarning("Order with ID 6 not found for user {UserId}", userId);
                    return NotFound("Order ID 6 not found");
                }

              //  _logger.LogInformation("Returning order ID {OrderId} for user {UserId}", firstOrder.Id, userId);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching orders for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetMyOrdersv2([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            string userId = "admin";

            _logger.LogInformation("GetMyOrdersv2 called for user {UserId}", userId);
            var orders = await _orderService.GetUserOrdersAsync(userId);
            var firstOrder = orders.FirstOrDefault(o => o.Id == 6);

            return Ok(MapToOrderDto(firstOrder));
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
