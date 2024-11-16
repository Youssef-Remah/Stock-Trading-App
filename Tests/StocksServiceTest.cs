using ServiceContracts.DTO;
using Services;
using Xunit;

namespace Tests
{
    public class StocksServiceTest
    {
        private readonly StocksService _stocksService;


        public StocksServiceTest()
        {
            _stocksService = new();
        }


        private BuyOrderRequest CreateDefaultBuyOrderRequest()
        {
            return new()
            {
                StockName = "Microsoft",

                StockSymbol = "MSFT",

                DateAndTimeOfOrder = Convert.ToDateTime("2024-11-16"),

                Quantity = 500,

                Price = 500,
            };
        }


        private SellOrderRequest CreateDefaultSellOrderRequest()
        {
            return new()
            {
                StockSymbol = "MSFT",

                StockName = "Microsoft",

                DateAndTimeOfOrder = Convert.ToDateTime("2024-11-16"),

                Price = 500,

                Quantity = 500,
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

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest();

            buyOrderRequest.Quantity = quantity;


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

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest();

            buyOrderRequest.Quantity = quantity;


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

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest();

            buyOrderRequest.Price = price;


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

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest();

            buyOrderRequest.Price = price;


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

            BuyOrderRequest buyOrderRequest = CreateDefaultBuyOrderRequest();

            buyOrderRequest.DateAndTimeOfOrder = Convert.ToDateTime(orderDate);


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

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest();

            sellOrderRequest.Quantity = quantity;


            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => {
                //Act
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }


        // If Quantity is greater than the maximum (100,000), throws ArgumentException
        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_QuantityIsGreaterThanMinimum(uint quantity)
        {
            //Arrange

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest();

            sellOrderRequest.Quantity = quantity;


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

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest();

            sellOrderRequest.Price = price;


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

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest();

            sellOrderRequest.Price = price;


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

            SellOrderRequest sellOrderRequest = CreateDefaultSellOrderRequest();

            sellOrderRequest.DateAndTimeOfOrder = Convert.ToDateTime(orderDate);


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

    }
}