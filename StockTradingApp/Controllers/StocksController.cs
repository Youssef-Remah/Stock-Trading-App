using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly TradingOptions _tradingOptions;

        private readonly IFinnhubService _finnhubService;

        private readonly ILogger<StocksController> _logger;


        public StocksController(
            IOptions<TradingOptions> tradingOptions,

            IFinnhubService finnhubService,

            ILogger<StocksController> logger
        )
        {
            _tradingOptions = tradingOptions.Value;

            _finnhubService = finnhubService;

            _logger = logger;
        }


        [HttpGet]
        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            _logger.LogInformation("In {ControllerName}.{ActionMethodName}() action method",
                nameof(StocksController), nameof(Explore));

            _logger.LogDebug("stock: {stock}, showAll: {showAll}", stock, showAll);


            List<Dictionary<string, string>>? stocksDictionary = await _finnhubService.GetStocks();

            List<Stock> stocks = new List<Stock>();

            if (stocksDictionary is not null)
            {              
                if (!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(",");
                    if (Top25PopularStocksList is not null)
                    {
                        stocksDictionary = stocksDictionary
                         .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
                         .ToList();
                    }
                }
                
                stocks = stocksDictionary
                 .Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
                .ToList();
            }

            ViewBag.stock = stock;

            return View(stocks);
        }
    }
}
