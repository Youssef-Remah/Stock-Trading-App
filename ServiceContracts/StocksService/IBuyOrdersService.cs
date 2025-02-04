using ServiceContracts.DTO;

namespace ServiceContracts.StocksService
{
    /// <summary>
    /// Represents Stocks service that includes operations for buying orders
    /// </summary>
    public interface IBuyOrdersService
    {
        /// <summary>
        /// Creates a buy order
        /// </summary>
        /// <param name="buyOrderRequest">Object of type BuyOrderRequest</param>
        /// <returns>Returns a BuyOrderResponse object</returns>
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);


        /// <summary>
        /// Returns all existing buy orders
        /// </summary>
        /// <returns>Returns a list of objects of BuyOrderResponse type</returns>
        public Task<List<BuyOrderResponse>> GetBuyOrders();
    }
}