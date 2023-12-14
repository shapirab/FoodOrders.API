using FoodOrders.API.Data.DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrders.API.Data.DbContexts
{
    public class FoodOrdersDbContext : DbContext
    {
        public FoodOrdersDbContext(DbContextOptions<FoodOrdersDbContext> options) : base(options)
        {
            
        }
        public DbSet<CustomerEntity>Customers { get; set; }
        public DbSet<ShoppingCartContentEntity> ShoppingCartContents { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
                .HasMany(x => x.Content)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerID)
                .IsRequired();
        }
    }
}
