﻿using ServiceContracts.DTO;
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

            BuyOrderRequest buyOrderRequest = new()
            {
                StockName = "Microsoft",

                StockSymbol = "MSFT",

                DateAndTimeOfOrder = Convert.ToDateTime("2000-01-01"),

                Quantity = quantity,

                Price = 500,
            };

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

            BuyOrderRequest buyOrderRequest = new()
            {
                StockName = "Microsoft",

                StockSymbol = "MSFT",

                DateAndTimeOfOrder = Convert.ToDateTime("2000-01-01"),

                Quantity = quantity,

                Price = 500,
            };

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

            BuyOrderRequest buyOrderRequest = new()
            {
                StockName = "Microsoft",

                StockSymbol = "MSFT",

                DateAndTimeOfOrder = Convert.ToDateTime("2000-01-01"),

                Quantity = 500,

                Price = price,
            };


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

            BuyOrderRequest buyOrderRequest = new()
            {
                StockName = "Microsoft",

                StockSymbol = "MSFT",

                DateAndTimeOfOrder = Convert.ToDateTime("2000-01-01"),

                Quantity = 500,

                Price = price,
            };


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

            BuyOrderRequest buyOrderRequest = new()
            {
                StockName = "Microsoft",

                StockSymbol = null,

                DateAndTimeOfOrder = Convert.ToDateTime("2000-01-01"),

                Quantity = 500,

                Price = 500,
            };


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

            BuyOrderRequest buyOrderRequest = new()
            {
                StockName = "Microsoft",

                StockSymbol = null,

                DateAndTimeOfOrder = Convert.ToDateTime(orderDate),

                Quantity = 500,

                Price = 500,
            };


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

            BuyOrderRequest buyOrderRequest = new()
            {
                StockName = "Microsoft",

                StockSymbol = null,

                DateAndTimeOfOrder = Convert.ToDateTime("2024-11-16"),

                Quantity = 500,

                Price = 500,
            };


            //Act

            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);


            //Assert

            Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID);
        }

        #endregion

    }
}