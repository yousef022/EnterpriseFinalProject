//Author: Yousef Osman
using Microsoft.EntityFrameworkCore;

namespace ANotSoTypicalMarketplace.Models
{
    public class Database : DbContext
    {
        public Database()
        {

        }

        public Database(DbContextOptions<Database> contextOptions)
            : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }

        //Datastructure
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PriceMatch> PriceMatches { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

    }
}
