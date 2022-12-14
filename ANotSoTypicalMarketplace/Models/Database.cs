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

        //Datastructure
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PriceMatch> PriceMatches { get; set; }

        public DbSet<Cart> Carts { get; set; }

    }
}
