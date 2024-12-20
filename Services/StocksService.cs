using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksService(IStocksRepository stocksRepository)
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

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();

            List<BuyOrderResponse> buyOrderResponses = buyOrders.Select(buyOrder => 
            buyOrder.ToBuyOrderResponse()).ToList();

            return await Task.FromResult(buyOrderResponses);
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