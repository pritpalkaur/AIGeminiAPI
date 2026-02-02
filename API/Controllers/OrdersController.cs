using API.BusinessLayer;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Controllers/OrdersController.cs
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // require JWT
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userId = User.Identity?.Name; // or claim "sub"/"username"
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var order = await _orderService.CreateOrderAsync(userId, dto);

            var response = MapToResponse(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var order = await _orderService.GetOrderAsync(id, userId);
            if (order == null) return NotFound();

            return Ok(MapToResponse(order));
        }

        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            //int userId = 1;
            var orders = await _orderService.GetUserOrdersAsync(userId.ToString());
            return Ok(orders.Select(MapToResponse));
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
