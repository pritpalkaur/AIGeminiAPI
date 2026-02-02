using API.DTOs;

namespace API.DataAccessLayer
{
    // DataAccess/IOrderRepository.cs
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id, string userId);
        Task<List<Order>> GetByUserAsync(string userId);
        Task<Order> AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
}
