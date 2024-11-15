using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents Stocks service that includes operations like buy order and sell order
    /// </summary>
    public interface IStocksService
    {
        /// <summary>
        /// Creates a buy order
        /// </summary>
        /// <param name="buyOrderRequest">Object of type BuyOrderRequest</param>
        /// <returns>Returns a BuyOrderResponse object</returns>
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);


        /// <summary>
        /// Creates a sell order
        /// </summary>
        /// <param name="sellOrderRequest">Object of type SellOrderRequest</param>
        /// <returns>Returns a SellOrderResponse object</returns>
        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);


        /// <summary>
        /// Returns all existing buy orders
        /// </summary>
        /// <returns>Returns a list of objects of BuyOrderResponse type</returns>
        public Task<List<BuyOrderResponse>> GetBuyOrders();


        /// <summary>
        /// Returns all existing sell orders
        /// </summary>
        /// <returns>Returns a list of objects of SellOrderResponse type</returns>
        public Task<List<SellOrderResponse>> GetSellOrders();
    }
}