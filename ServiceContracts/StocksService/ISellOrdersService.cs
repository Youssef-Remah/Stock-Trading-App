using ServiceContracts.DTO;

namespace ServiceContracts.StocksService
{
    public interface ISellOrdersService
    {
        /// <summary>
        /// Creates a sell order
        /// </summary>
        /// <param name="sellOrderRequest">Object of type SellOrderRequest</param>
        /// <returns>Returns a SellOrderResponse object</returns>
        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);


        /// <summary>
        /// Returns all existing sell orders
        /// </summary>
        /// <returns>Returns a list of objects of SellOrderResponse type</returns>
        public Task<List<SellOrderResponse>> GetSellOrders();
    }
}