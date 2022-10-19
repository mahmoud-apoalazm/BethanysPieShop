using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class BethanyPieShopDbContext: IdentityDbContext
    {
        public BethanyPieShopDbContext(DbContextOptions<BethanyPieShopDbContext> options)
            :base(options)
        {

        } 
        public DbSet<Category> Categories { get; set; }=null!;
        public DbSet<Pie> Pies { get; set; } = null!;
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    }
}
