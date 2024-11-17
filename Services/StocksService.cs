using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;

        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new();

            _sellOrders = new();
        }


        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            //check if buyOrderRequest is null
            ArgumentNullException.ThrowIfNull(nameof(buyOrderRequest));

            //Model Validation
            ValidationHelper.ModelValidator(buyOrderRequest!);

            //Convert BuyOrderRequest to BuyOrder entity object
            BuyOrder buyOrder = buyOrderRequest!.ToBuyOrder();

            //Assign the BuyOrder item a new generated ID
            buyOrder.BuyOrderID = Guid.NewGuid();

            //Insert BuyOrder item into _buyOrders list
            _buyOrders.Add(buyOrder);

            //Convert BuyOrder item to BuyOrderResponse
            BuyOrderResponse buyOrderResponse = buyOrder.ToBuyOrderResponse();

            //Return BuyOrderResponse object
            return Task.FromResult(buyOrderResponse);
        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            //If sellOrderRequest is null throw ArgumentNullException
            ArgumentNullException.ThrowIfNull(sellOrderRequest);

            //Model Validation
            ValidationHelper.ModelValidator(sellOrderRequest!);

            //Convert sellOrderRequest to SellOrder type
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //Assign the SellOrder item a new generated ID
            sellOrder.SellOrderID = Guid.NewGuid();

            //Insert the SellOrder item into the _sellOrders list
            _sellOrders.Add(sellOrder);

            //Convert the SellOrder item into SellOrderResponse type
            SellOrderResponse sellOrderResponse = sellOrder.ToSellOrderResponse();

            //Return the SellOrderResponse object
            return Task.FromResult(sellOrderResponse);
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> buyOrderResponses = 
                _buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();

            return Task.FromResult(buyOrderResponses);
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> sellOrderResponses =
                _sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();

            return Task.FromResult(sellOrderResponses);
        }
    }
}