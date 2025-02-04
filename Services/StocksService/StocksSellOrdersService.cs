using Entities;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.StocksService;
using Services.Helpers;

namespace Services.StocksService
{
    public class StocksSellOrdersService : ISellOrdersService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksSellOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }


        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            //If sellOrderRequest is null throw ArgumentNullException
            ArgumentNullException.ThrowIfNull(sellOrderRequest);

            //Model Validation
            ValidationHelper.ModelValidator(sellOrderRequest!);

            //Convert sellOrderRequest to SellOrder type
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //Assign the SellOrder item a new generated ID
            sellOrder.SellOrderID = Guid.NewGuid();

            //Insert the SellOrder item into the database table called 'SellOrders'
            await _stocksRepository.CreateSellOrder(sellOrder);

            //Return the SellOrderResponse object
            return await Task.FromResult(sellOrder.ToSellOrderResponse());
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _stocksRepository.GetSellOrders();

            List<SellOrderResponse> sellOrderResponses = sellOrders.Select(
                sellOrder => sellOrder.ToSellOrderResponse()).ToList();

            return await Task.FromResult(sellOrderResponses);
        }
    }
}