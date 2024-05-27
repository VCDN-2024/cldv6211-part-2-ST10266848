using KhumaloCraft_POE_P2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace KhumaloCraft_POE_P2.Data
{
    public class KhumaloCraftDbContext : IdentityDbContext
    {
        public KhumaloCraftDbContext(DbContextOptions options) : base(options)
        {  
        }

        //public DbSet<IdentityUser> Users { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;

        public DbSet<OrderHistory> OrderHistories { get; set; } = default!;
    }
}
