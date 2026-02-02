using API.DataAccessLayer;
using API.DTOs;

namespace API.BusinessLayer
{
    // Services/OrderService.cs
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<Order> CreateOrderAsync(string userId, CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending.ToString(),
                Items = dto.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            order.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);

            return await _repo.AddAsync(order);
        }

        public Task<Order> GetOrderAsync(int id, string userId) =>
            _repo.GetByIdAsync(id, userId);

        public Task<List<Order>> GetUserOrdersAsync(string userId) =>
            _repo.GetByUserAsync(userId);
    }
}
