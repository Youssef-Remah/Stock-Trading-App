using Entities;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.StocksService;
using Services.Helpers;

namespace Services.StocksService
{
    public class StocksBuyOrdersService : IBuyOrdersService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksBuyOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            //check if buyOrderRequest is null
            ArgumentNullException.ThrowIfNull(nameof(buyOrderRequest));

            //Model Validation
            ValidationHelper.ModelValidator(buyOrderRequest!);

            //Convert BuyOrderRequest to BuyOrder entity object
            BuyOrder buyOrder = buyOrderRequest!.ToBuyOrder();

            //Assign the BuyOrder item a new generated ID
            buyOrder.BuyOrderID = Guid.NewGuid();

            //Insert BuyOrder item into the database table 'BuyOrders'
            await _stocksRepository.CreateBuyOrder(buyOrder);

            //Return BuyOrderResponse object
            return await Task.FromResult(buyOrder.ToBuyOrderResponse());
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();

            List<BuyOrderResponse> buyOrderResponses = buyOrders.Select(buyOrder =>
            buyOrder.ToBuyOrderResponse()).ToList();

            return await Task.FromResult(buyOrderResponses);
        }
    }
}