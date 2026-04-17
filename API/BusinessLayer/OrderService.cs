using API.DataAccessLayer;
using API.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace API.BusinessLayer
{
    // Services/OrderService.cs
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMemoryCache _cache;

        public OrderService(IOrderRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
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
        public async Task<PagedResult<Order>> GetPagedOrdersAsync(string userId, int pageNumber, int pageSize)
        {
            string cacheKey = $"orders_{userId}_page_{pageNumber}_size_{pageSize}";

            // Try to get cached value
            if (_cache.TryGetValue(cacheKey, out PagedResult<Order> cachedResult))
            {
                return cachedResult;
            }

            // Not in cache → fetch from DB
            var result = await _repo.GetPagedOrdersAsync(userId, pageNumber, pageSize);

            // Cache it for 60 seconds
            _cache.Set(cacheKey, result, TimeSpan.FromSeconds(60));

            return result;
        }

        public Task<Order> GetOrderAsync(int id, string userId) =>
            _repo.GetByIdAsync(id, userId);

        public Task<List<Order>> GetUserOrdersAsync(string userId) =>
            _repo.GetByUserAsync(userId);
    }
}
