using API.DTOs;
using API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace API.DataAccessLayer
{
    public class OrderDbContext : DbContext
    {
        // Constructor accepting DbContextOptions
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        // DbSet for each entity/table
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        // Optional: Override OnModelCreating if needed for custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configuration if needed
        }
    }
}
