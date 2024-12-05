using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class StockMarketDbContext : DbContext
    {
        public StockMarketDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<BuyOrder> BuyOrders { get; set; }

        public DbSet<SellOrder> SellOrders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyOrder>().ToTable(nameof(BuyOrders));

            modelBuilder.Entity<SellOrder>().ToTable(nameof(SellOrders));
        }
    }
}