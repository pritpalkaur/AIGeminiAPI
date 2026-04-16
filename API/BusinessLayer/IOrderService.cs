using API.DTOs;

namespace API.BusinessLayer
{
    // Services/IOrderService.cs
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId, CreateOrderDto dto);
        Task<Order> GetOrderAsync(int id, string userId);
        Task<List<Order>> GetUserOrdersAsync(string userId);
        Task<PagedResult<Order>> GetPagedOrdersAsync(string userId, int pageNumber, int pageSize);
    }
}
