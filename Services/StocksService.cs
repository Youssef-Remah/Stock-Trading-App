using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly StockMarketDbContext _db;

        public StocksService(StockMarketDbContext stockMarketDbContext)
        {
            _db = stockMarketDbContext;
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
            await _db.BuyOrders.AddAsync(buyOrder);

            await _db.SaveChangesAsync();

            //Convert BuyOrder item to BuyOrderResponse
            BuyOrderResponse buyOrderResponse = buyOrder.ToBuyOrderResponse();

            //Return BuyOrderResponse object
            return await Task.FromResult(buyOrderResponse);
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
            await _db.SellOrders.AddAsync(sellOrder);

            await _db.SaveChangesAsync();

            //Convert the SellOrder item into SellOrderResponse type
            SellOrderResponse sellOrderResponse = sellOrder.ToSellOrderResponse();

            //Return the SellOrderResponse object
            return await Task.FromResult(sellOrderResponse);
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> buyOrderResponses =
                await _db.BuyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToListAsync();

            return await Task.FromResult(buyOrderResponses);
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> sellOrderResponses =
                await _db.SellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToListAsync();

            return await Task.FromResult(sellOrderResponses);
        }
    }
}