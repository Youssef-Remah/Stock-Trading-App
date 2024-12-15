using ServiceContracts.DTO;
using Services;
using System.Runtime.InteropServices;
using Xunit;

namespace Tests
{
    public class StocksServiceTest
    {
        private readonly StocksService _stocksService;


        public StocksServiceTest()
        {
            _stocksService = new(null);
        }


        private BuyOrderRequest CreateDefaultBuyOrderRequest([Optional] string? stockName,
            [Optional]  string? stockSymbol, [Optional] DateTime? dateAndTimeOfOrder,
            [Optional] uint? quantity, [Optional] double? price)
        {
            return new()
            {
                StockName = stockName ?? "Microsoft",

                StockSymbol = stockSymbol ?? "MSFT",

                DateAndTimeOfOrder = dateAndTimeOfOrder ?? Convert.ToDateTime("2024-11-16"),

                Quantity = quantity ?? 500,

                Price = price ?? 500
            };
        }


        private SellOrderRequest CreateDefaultSellOrderRequest([Optional] string? stockName,
            [Optional] string? stockSymbol, [Optional] DateTime? dateAndTimeOfOrder,
            [Optional] uint? quantity, [Optional] double? price)
        {
            return new()
            {
                StockName = stockName ?? "Microsoft",

                StockSymbol = stockSymbol ?? "MSFT",

                DateAndTimeOfOrder = dateAndTimeOfOrder ?? Convert.ToDateTime("2024-11-16"),

                Quantity = quantity ?? 500,

                Price = price ?? 500
            };
        }


        #region CreateBuyOrder

        // If BuyOrderRequest is null, throw ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder()
        {
            //Arrange

            BuyOrderRequest? buyOrderRequest = null;


            //Assert

            await Assert.ThrowsAsync<ArgumentNullException>(() => {
                //Act
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        // If Quantity is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_QuantityIsLessThanMinimum(uint quantity)
        {
            //Arrange

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest(quantity: quantity);


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        // If Quantity is greater than the maximum (100,000), throws ArgumentException
        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyOrder_QuantityIsGreaterThanMaximum(uint quantity)
        {
            //Arrange

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest(quantity: quantity);


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        // If Price is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_PriceIsLessThanMinimum(double price)
        {
            //Arrange

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest(price: price);


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        // If Price is greater than maximum (10,000), throws ArgumentException
        [Theory]
        [InlineData(10001)]
        public async Task CreateBuyOrder_PriceIsGreaterThanMaximum(double price)
        {
            //Arrange

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest(price: price);


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        // If StockSymbol is null, throws ArgumentException
        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull()
        {
            //Arrange

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest();

            buyOrderRequest.StockSymbol = null;


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        // If DateAndTimeOfOrder is less than minimum year (2000), throws ArgumentException
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateBuyOrder_DateOfOrderIsLessThanMinimumYear(string orderDate)
        {
            //Arrange

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest(dateAndTimeOfOrder: 
                Convert.ToDateTime(orderDate));


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        // If all properties of BuyOrderRequest object are valid,
        // return a new object of BuyOrderResponse with a new generated Guid
        [Fact]
        public async void CreateBuyOrder_ValidData_ToBeSuccessful()
        {
            //Arrange

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest();


            //Act

            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);


            //Assert

            Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID);
        }

        #endregion


        #region CreateSellOrder

        // If SellOrderRequest is null, throws ArgumentNullException
        [Fact]
        public async Task CreateSellOrder_NullSellOrder()
        {
            //Arrange

            SellOrderRequest? sellOrderRequest = null;


            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If Quantity is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_QuantityIsLessThanMinimum(uint quantity)
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest(quantity: quantity);


            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If Quantity is greater than the maximum (100,000), throws ArgumentException
        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_QuantityIsGreaterThanMaximum(uint quantity)
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest(quantity: quantity);


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If Price is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_PriceIsLessThanMinimum(double price)
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest(price: price);


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If Price is greater than maximum (10,000), throws ArgumentException
        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_PriceIsGreaterThanMaximum(double price)
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest(price: price);


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If StockSymbol is null, throws ArgumentException
        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull()
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest();

            sellOrderRequest.StockSymbol = null;


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If DateAndTimeOfOrder is less than minimum year (2000), throws ArgumentException
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateSellOrder_DateOfOrderIsLessThanMinimumYear(string orderDate)
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest(dateAndTimeOfOrder: 
                Convert.ToDateTime(orderDate));


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If all properties of SellOrderRequest object are valid,
        // return a new object of SellOrderResponse with a new generated Guid
        [Fact]
        public async Task CreateSellOrder_ValidData_ToBeSuccessful()
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest();


            //Act
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);


            //Assert
            Assert.NotEqual(Guid.Empty, sellOrderResponse.SellOrderID);
        }

        #endregion


        #region GetBuyOrders

        // By default when GetBuyOrders() get called, it should return an empty list
        [Fact]
        public async Task GetBuyOrders_EmptyList()
        {
            //Act

            List<BuyOrderResponse> buyOrdersList = await _stocksService.GetBuyOrders();


            //Assert

            Assert.Empty(buyOrdersList);
        }


        // When some BuyOrder items are added using CreateBuyOrder(),
        // the returned list from GetBuyOrders() should contain all the recently added items
        [Fact]
        public async Task GetBuyOrders_ValidData_ToBeSuccessful()
        {
            //Arrange

            BuyOrderRequest buyOrderRequest1 = CreateDefaultBuyOrderRequest();

            BuyOrderRequest buyOrderRequest2 = CreateDefaultBuyOrderRequest(stockName: "Apple", stockSymbol: "AAPL");


            List<BuyOrderRequest> buyOrderRequests = new() { buyOrderRequest1, buyOrderRequest2 };

            List<BuyOrderResponse> buyOrderResponses = new();


            foreach (BuyOrderRequest req in buyOrderRequests)
            {
                buyOrderResponses.Add(await _stocksService.CreateBuyOrder(req));
            }


            //Act

            List<BuyOrderResponse> buyOrderResponses_FromGet = await _stocksService.GetBuyOrders();


            //Assert

            foreach (BuyOrderResponse res in buyOrderResponses)
            {
                Assert.Contains(res, buyOrderResponses_FromGet);
            }
        }

        #endregion


        #region GetSellOrders

        // By default when GetSellOrders() get called, it should return an empty list
        [Fact]
        public async Task GetSellOrders_EmptyList()
        {
            //Act

            List<SellOrderResponse> sellOrdersList = await _stocksService.GetSellOrders();


            //Assert

            Assert.Empty(sellOrdersList);
        }


        // When some SellOrder items are added using CreateSellOrder(),
        // the returned list from GetSellOrders() should contain all the recently added items
        [Fact]
        public async Task GetSellOrders_ValidData_ToBeSuccessful()
        {
            //Arrange

            SellOrderRequest sellOrderRequest1 = CreateDefaultSellOrderRequest();

            SellOrderRequest sellOrderRequest2 = CreateDefaultSellOrderRequest(stockName: "Apple", stockSymbol: "AAPL");


            List<SellOrderRequest> sellOrderRequests = new() { sellOrderRequest1, sellOrderRequest2 };

            List<SellOrderResponse> sellOrderResponses = new();


            foreach (SellOrderRequest req in sellOrderRequests)
            {
                sellOrderResponses.Add(await _stocksService.CreateSellOrder(req));
            }


            //Act

            List<SellOrderResponse> sellOrderResponses_FromGet = await _stocksService.GetSellOrders();


            //Assert

            foreach (SellOrderResponse res in sellOrderResponses)
            {
                Assert.Contains(res, sellOrderResponses_FromGet);
            }
        }

        #endregion
    }
}