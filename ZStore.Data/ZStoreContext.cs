using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZStore.Core;

namespace ZStore.Data
{
    public class ZStoreContext:IdentityDbContext<CustomUser>
    {
        public ZStoreContext(DbContextOptions options):base(options){}
        public DbSet<CustomUser> CustomUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

    }
}