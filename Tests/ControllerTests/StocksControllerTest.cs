using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using ServiceContracts;
using StockTradingApp;
using StockTradingApp.Controllers;
using StockTradingApp.Models;
using System.Text.Json;

namespace Tests.ControllerTests
{
    public class StocksControllerTest
    {
        private IOptions<TradingOptions>? _tradingOptions;

        private readonly Mock<IFinnhubService> _finnhubServiceMock;

        private readonly IFinnhubService _finnhubService;

        private StocksController? _stocksController;


        public StocksControllerTest()
        {
            _finnhubServiceMock = new();

            _finnhubService = _finnhubServiceMock.Object;
        }


        #region Explore

        [Fact]
        public async Task Explore_StockIsNull_ShouldReturnExploreViewWithStocksList()
        {
            //Arrange
            _tradingOptions = Options.Create(new TradingOptions() { DefaultOrderQuantity = 100, Top25PopularStocks = "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO" });

            _stocksController = new(_tradingOptions, _finnhubService);

            List<Dictionary<string, string>>? stocksDict = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(@"[{""currency"":""USD"",""description"":""APPLE INC"",""displaySymbol"":""AAPL"",""figi"":""BBG000B9XRY4"",""isin"":null,""mic"":""XNAS"",""shareClassFIGI"":""BBG001S5N8V8"",""symbol"":""AAPL"",""symbol2"":"""",""type"":""Common Stock""},{""currency"":""USD"",""description"":""MICROSOFT CORP"",""displaySymbol"":""MSFT"",""figi"":""BBG000BPH459"",""isin"":null,""mic"":""XNAS"",""shareClassFIGI"":""BBG001S5TD05"",""symbol"":""MSFT"",""symbol2"":"""",""type"":""Common Stock""},{""currency"":""USD"",""description"":""AMAZON.COM INC"",""displaySymbol"":""AMZN"",""figi"":""BBG000BVPV84"",""isin"":null,""mic"":""XNAS"",""shareClassFIGI"":""BBG001S5PQL7"",""symbol"":""AMZN"",""symbol2"":"""",""type"":""Common Stock""},{""currency"":""USD"",""description"":""TESLA INC"",""displaySymbol"":""TSLA"",""figi"":""BBG000N9MNX3"",""isin"":null,""mic"":""XNAS"",""shareClassFIGI"":""BBG001SQKGD7"",""symbol"":""TSLA"",""symbol2"":"""",""type"":""Common Stock""},{""currency"":""USD"",""description"":""ALPHABET INC-CL A"",""displaySymbol"":""GOOGL"",""figi"":""BBG009S39JX6"",""isin"":null,""mic"":""XNAS"",""shareClassFIGI"":""BBG009S39JY5"",""symbol"":""GOOGL"",""symbol2"":"""",""type"":""Common Stock""}]");

            List<Stock> expectedStocks = stocksDict!.Select(dict => 
                new Stock() { StockName = Convert.ToString(dict["description"]),
                StockSymbol = Convert.ToString(dict["symbol"]) }).ToList();

            //Mock
            _finnhubServiceMock.Setup(func => func.GetStocks()).ReturnsAsync(stocksDict);

            //Act
            IActionResult actionResult = await _stocksController.Explore(null, true);

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(expectedStocks);
        }

        #endregion
    }
}