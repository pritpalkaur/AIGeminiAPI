    using API.DTOs;
    using Microsoft.EntityFrameworkCore;
    //using OrderService.DataAccess;
    //using OrderService.Domain;
namespace API.DataAccessLayer
{


    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetByIdAsync(int id, string userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
        }

        public async Task<List<Order>> GetByUserAsync(string userId)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.Items)
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Optional: log the error (ILogger recommended)
                Console.WriteLine($"Error in GetByUserAsync: {ex.Message}");

                // Option 1: return empty list (safe for UI)
                return new List<Order>();

                // Option 2: rethrow if you want controller to handle it
                // throw;
            }

        }
        public async Task<PagedResult<Order>> GetPagedOrdersAsync(PaginationParams pagination, string? userId = null)
        {
            var query = _context.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(o => o.UserId == userId);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new PagedResult<Order>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
