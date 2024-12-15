using Entities;

namespace RepositoryContracts
{
    /// <summary>
    /// Represents Stocks service that includes operations like buy order, sell order
    /// </summary>
    public interface IStocksRepository
    {
        /// <summary>
        /// Inserts a new buy order into the database table "BuyOrders"
        /// </summary>
        /// <param name="buyOrder">Buy order object</param>
        /// <returns>Returns the inserted buy order</returns>
        public Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);


        /// <summary>
        /// Inserts a new sell order into the database table called "SellOrders"
        /// </summary>
        /// <param name="sellOrder">Sell order object</param>
        /// <returns>Returns the inserted sell order</returns>
        public Task<SellOrder> CreateSellOrder(SellOrder sellOrder);


        /// <summary>
        /// Returns the existing list of buy orders retrieved from database table called "BuyOrders"
        /// </summary>
        /// <returns>Returns a list of objects of BuyOrder type</returns>
        public Task<List<BuyOrder>> GetBuyOrders();


        /// <summary>
        /// Returns the existing list of sell orders retrieved from database table called "SellOrders"
        /// </summary>
        /// <returns>Returns a list of objects of SellOrder type</returns>
        public Task<List<SellOrder>> GetSellOrders();
    }
}