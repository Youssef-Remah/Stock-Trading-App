using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly TradingOptions _tradingOptions;

        private readonly IFinnhubService _finnhubService;

        private readonly IStocksService _stocksService;


        public StocksController(
            TradingOptions tradingOptions,
            IFinnhubService finnhubService,
            IStocksService stocksService
        )
        {
            _tradingOptions = tradingOptions;

            _finnhubService = finnhubService;

            _stocksService = stocksService;
        }


        [HttpGet]
        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            List<Dictionary<string, string>>? stocksDictionary = await _finnhubService.GetStocks();

            List<Stock> stocks = new();

            if (stocksDictionary is not null)
            {
                if (!showAll && _tradingOptions.Top25PopularStocks is not null)
                {
                    string[]? top25PopularStocks = _tradingOptions.Top25PopularStocks.Split(',');

                    if(top25PopularStocks is not null)
                    {
                        stocksDictionary = stocksDictionary.Where(
                            stock => top25PopularStocks.Contains(stock["symbol"])).ToList();
                    }
                }

                stocks = stocksDictionary.Select(
                stock => new Stock()
                {
                    StockName = Convert.ToString(stock["description"]),
                    StockSymbol = Convert.ToString(stock["symbol"])
                }).ToList();
            }

            ViewBag.stock = stock;

            return View(stocks);
        }
    }
}
