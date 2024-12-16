using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class StocksRepository : IStocksRepository
    {
        private readonly StockMarketDbContext _db;

        public StocksRepository(StockMarketDbContext stockMarketDbContext)
        {
            _db = stockMarketDbContext;
        }

        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            await _db.BuyOrders.AddAsync(buyOrder);

            await _db.SaveChangesAsync();

            return buyOrder;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            await _db.SellOrders.AddAsync(sellOrder);

            await _db.SaveChangesAsync();

            return sellOrder;
        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            return await _db.BuyOrders.OrderByDescending(buyOrder => buyOrder.DateAndTimeOfOrder).ToListAsync();
        }

        public async Task<List<SellOrder>> GetSellOrders()
        {
            return await _db.SellOrders.OrderByDescending(sellOrder => sellOrder.DateAndTimeOfOrder).ToListAsync();
        }
    }
}