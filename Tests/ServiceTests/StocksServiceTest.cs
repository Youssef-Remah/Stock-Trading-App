using AutoFixture;
using Moq;
using RepositoryContracts;
using ServiceContracts.DTO;
using Xunit;
using FluentAssertions;
using Entities;
using ServiceContracts.StocksService;
using Services.StocksService;

namespace Tests.ServiceTests
{
    public class StocksServiceTest
    {
        private readonly IBuyOrdersService _stocksBuyOrdersService;

        private readonly ISellOrdersService _stocksSellOrdersService;

        private readonly Mock<IStocksRepository> _stocksRepositoryMock;

        private readonly IStocksRepository _stocksRepository;

        private readonly IFixture _fixture;


        public StocksServiceTest()
        {
            _stocksRepositoryMock = new();

            _stocksRepository = _stocksRepositoryMock.Object;

            _stocksBuyOrdersService = new StocksBuyOrdersService(_stocksRepository);

            _stocksSellOrdersService = new StocksSellOrdersService(_stocksRepository);

            _fixture = new Fixture();
        }


        #region CreateBuyOrder

        // If BuyOrderRequest is null, throw ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = null;

            //Mock
            BuyOrder buyOrderFixture = _fixture.Build<BuyOrder>().Create();

            _stocksRepositoryMock.Setup(func => func.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrderFixture);

            //Act
            Func<Task<BuyOrderResponse>> action = async () =>
            {
                return await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
            

        // If Quantity is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint quantity)
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                      .With(p => p.Quantity, quantity)
                                                      .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateBuyOrder(It.IsAny<BuyOrder>()))
                                                    .ReturnsAsync(buyOrderRequest.ToBuyOrder());

            //Act
            Func<Task<BuyOrderResponse>> action = async () =>
            {
                return await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If Quantity is greater than the maximum (100,000), throws ArgumentException
        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint quantity)
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                      .With(p => p.Quantity, quantity)
                                                      .Create();

            //Mock
            _stocksRepositoryMock.Setup(fun => fun.CreateBuyOrder(It.IsAny<BuyOrder>()))
                                 .ReturnsAsync(buyOrderRequest.ToBuyOrder());

            //Act
            Func<Task<BuyOrderResponse>> action = async () =>
            {
                return await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If Price is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(double price)
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                      .With(p => p.Price, price)
                                                      .Create();

            //Mock
            _stocksRepositoryMock.Setup(fun => fun.CreateBuyOrder(It.IsAny<BuyOrder>()))
                                 .ReturnsAsync(buyOrderRequest.ToBuyOrder());

            //Act
            Func<Task<BuyOrderResponse>> action = async () =>
            {
                return await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If Price is greater than maximum (10,000), throws ArgumentException
        [Theory]
        [InlineData(10001)]
        public async Task CreateBuyOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(double price)
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                      .With(p => p.Price, price)
                                                      .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateBuyOrder(It.IsAny<BuyOrder>()))
                                 .ReturnsAsync(buyOrderRequest.ToBuyOrder());

            //Act
            Func<Task<BuyOrderResponse>> action = async () =>
            {
                return await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If StockSymbol is null, throws ArgumentException
        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                      .With(p => p.StockSymbol, null as string)
                                                      .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateBuyOrder(It.IsAny<BuyOrder>()))
                                                    .ReturnsAsync(buyOrderRequest.ToBuyOrder());

            //Act
            Func<Task<BuyOrderResponse>> action = async () =>
            {
                return await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If DateAndTimeOfOrder is less than minimum year (2000), throws ArgumentException
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateBuyOrder_DateOfOrderIsLessThanMinimumYear_ToBeArgumentException(string orderDate)
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                                      .With(P => P.DateAndTimeOfOrder, Convert.ToDateTime(orderDate))
                                                      .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateBuyOrder(It.IsAny<BuyOrder>()))
                                                    .ReturnsAsync(buyOrderRequest.ToBuyOrder());

            //Act
            Func<Task<BuyOrderResponse>> action = async () =>
            {
                return await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If all properties of BuyOrderRequest object are valid,
        // return a new object of BuyOrderResponse with a new generated Guid
        [Fact]
        public async void CreateBuyOrder_ValidData_ToBeSuccessful()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>().Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateBuyOrder(It.IsAny<BuyOrder>()))
                                                    .ReturnsAsync(buyOrder);

            //Act
            BuyOrderResponse buyOrderResponse = await _stocksBuyOrdersService.CreateBuyOrder(buyOrderRequest);

            //Assert
            buyOrder.BuyOrderID = buyOrderResponse.BuyOrderID;

            buyOrderResponse.BuyOrderID.Should().NotBe(Guid.Empty);

            buyOrderResponse.Should().Be(buyOrder.ToBuyOrderResponse());
        }

        #endregion


        #region CreateSellOrder

        // If SellOrderRequest is null, throws ArgumentNullException
        [Fact]
        public async Task CreateSellOrder_NullSellOrder_ToBeArgumentNullException()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = null;

            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task<SellOrderResponse>> action = async () =>
            {
                return await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);
            };
            
            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }


        // If Quantity is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint quantity)
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                        .With(p => p.Quantity, quantity)
                                                        .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrderRequest.ToSellOrder());

            //Act
            Func<Task<SellOrderResponse>> action = async () =>
            {
                return await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If Quantity is greater than the maximum (100,000), throws ArgumentException
        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint quantity)
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                        .With(p => p.Quantity, quantity)
                                                        .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrderRequest.ToSellOrder());

            //Act
            Func<Task<SellOrderResponse>> action = async () =>
            {
                return await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If Price is less than minimum (1), throws ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentException(double price)
        {
            //Arrange

            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                        .With(p => p.Price, price)
                                                        .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrderRequest.ToSellOrder());

            //Act
            Func<Task<SellOrderResponse>> action = async () =>
            {
                return await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If Price is greater than maximum (10,000), throws ArgumentException
        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(double price)
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                        .With(p => p.Price, price)
                                                        .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrderRequest.ToSellOrder());

            //Act
            Func<Task<SellOrderResponse>> action = async () =>
            {
                return await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If StockSymbol is null, throws ArgumentException
        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                        .With(p => p.StockSymbol, null as string)
                                                        .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrderRequest.ToSellOrder());

            //Act
            Func<Task<SellOrderResponse>> action = async () =>
            {
                return await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If DateAndTimeOfOrder is less than minimum year (2000), throws ArgumentException
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateSellOrder_DateOfOrderIsLessThanMinimumYear_ToBeArgumentException(string orderDate)
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                                        .With(p => p.DateAndTimeOfOrder, Convert.ToDateTime(orderDate))
                                                        .Create();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrderRequest.ToSellOrder());

            //Act
            Func<Task<SellOrderResponse>> action = async () =>
            {
                return await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // If all properties of SellOrderRequest object are valid,
        // return a new object of SellOrderResponse with a new generated Guid
        [Fact]
        public async Task CreateSellOrder_ValidData_ToBeSuccessful()
        {
            //Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>().Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //Mock
            _stocksRepositoryMock.Setup(func => func.CreateSellOrder(It.IsAny<SellOrder>()))
                                                    .ReturnsAsync(sellOrder);

            //Act
            SellOrderResponse sellOrderResponse = await _stocksSellOrdersService.CreateSellOrder(sellOrderRequest);

            //Assert
            sellOrder.SellOrderID = sellOrderResponse.SellOrderID;

            sellOrderResponse.SellOrderID.Should().NotBe(Guid.Empty);

            sellOrderResponse.Should().Be(sellOrder.ToSellOrderResponse());
        }

        #endregion


        #region GetBuyOrders

        // By default when GetBuyOrders() get called, it should return an empty list
        [Fact]
        public async Task GetBuyOrders_EmptyList()
        {
            //Mock
            _stocksRepositoryMock.Setup(func => func.GetBuyOrders()).ReturnsAsync(new List<BuyOrder>());

            //Act
            List<BuyOrderResponse> buyOrdersList = await _stocksBuyOrdersService.GetBuyOrders();

            //Assert
            buyOrdersList.Should().BeEmpty();
        }


        // When some BuyOrder items are added using CreateBuyOrder(),
        // the returned list from GetBuyOrders() should contain all the recently added items
        [Fact]
        public async Task GetBuyOrders_ValidData_ToBeSuccessful()
        {
            //Arrange
            List<BuyOrder> buyOrders = new()
            {
                _fixture.Build<BuyOrder>().Create(),

                _fixture.Build<BuyOrder>().Create()
            };

            List<BuyOrderResponse> buyOrderResponses = buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse())
                                                                .ToList();

            //Mock
            _stocksRepositoryMock.Setup(func => func.GetBuyOrders()).ReturnsAsync(buyOrders);

            //Act
            List<BuyOrderResponse> buyOrderResponses_FromGet = await _stocksBuyOrdersService.GetBuyOrders();

            //Assert
            buyOrderResponses_FromGet.Should().BeEquivalentTo(buyOrderResponses);
        }

        #endregion


        #region GetSellOrders

        // By default when GetSellOrders() get called, it should return an empty list
        [Fact]
        public async Task GetSellOrders_EmptyList()
        {
            //Mock
            _stocksRepositoryMock.Setup(func => func.GetSellOrders()).ReturnsAsync(new List<SellOrder>());

            //Act
            List<SellOrderResponse> sellOrdersList = await _stocksSellOrdersService.GetSellOrders();

            //Assert
            sellOrdersList.Should().BeEmpty();
        }


        // When some SellOrder items are added using CreateSellOrder(),
        // the returned list from GetSellOrders() should contain all the recently added items
        [Fact]
        public async Task GetSellOrders_ValidData_ToBeSuccessful()
        {
            //Arrange
            List<SellOrder> sellOrders = new()
            {
                _fixture.Build<SellOrder>().Create(),
                _fixture.Build<SellOrder>().Create()
            };

            List<SellOrderResponse> sellOrderResponses = sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse())
                                                                   .ToList();

            //Mock
            _stocksRepositoryMock.Setup(func => func.GetSellOrders()).ReturnsAsync(sellOrders);

            //Act
            List<SellOrderResponse> sellOrderResponses_FromGet = await _stocksSellOrdersService.GetSellOrders();

            //Assert
            sellOrderResponses_FromGet.Should().BeEquivalentTo(sellOrderResponses);
        }

        #endregion
    }
}