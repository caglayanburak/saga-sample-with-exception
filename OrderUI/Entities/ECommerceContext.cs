using CommonLib;
using Microsoft.EntityFrameworkCore;

namespace OrderUI.Entities
{
    public class ECommerceContext: DbContext
    {
            public DbSet<Order> Order { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("User ID=postgres;Password=Password1;Host=localhost;Port=5432;Database=ECommerce;");
        
    }
}
