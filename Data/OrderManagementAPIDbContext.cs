using Microsoft.EntityFrameworkCore;
using Order_Management_API.Models;

namespace Order_Management_API.Data
{
    public class OrderManagementAPIDbContext : DbContext
    {
        public OrderManagementAPIDbContext(DbContextOptions<OrderManagementAPIDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
