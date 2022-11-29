using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderSheet> OrderSheets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }
        public DbSet<UserWorkGroup> UserWorkGroups { get; set; }
	}
}