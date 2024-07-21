using Models;
using Microsoft.EntityFrameworkCore;

namespace Dbcontext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasMany(p => p.Products).WithMany(u => u.Users);
            //modelBuilder.Entity<Wishlist>().HasMany(p => p.Products);

        }
    }
}
