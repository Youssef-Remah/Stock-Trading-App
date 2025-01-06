using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{

	[Route("[controller]")]
	public class TradeController : Controller
	{
		private readonly IConfiguration _configuration;

		private readonly TradingOptions _tradingOptions;

		private readonly IFinnhubService _finnhubService;

		private readonly IStocksService _stocksService;

		private readonly ILogger<TradeController> _logger;


		public TradeController(
			IConfiguration configuration,

			IOptions<TradingOptions> tradingOptions,

			IFinnhubService finnhubService,

			IStocksService stocksService,

            ILogger<TradeController> logger
        )
		{
			_configuration = configuration;

			_tradingOptions = tradingOptions.Value;

			_finnhubService = finnhubService;

			_stocksService = stocksService;

			_logger = logger;
		}


		[HttpGet]
		[Route("[action]/{stockSymbol}")]
		[Route("~/[controller]/{stockSymbol}")]
		public async Task<IActionResult> Index(string stockSymbol)
		{
			_logger.LogInformation("In {ControllerName}.{ActionMethodName}() action method",
				nameof(TradeController), nameof(Index));

			_logger.LogDebug("StockSymbol: {stockSymbol}", stockSymbol);

			//reset stock symbol if not exists
			if (string.IsNullOrEmpty(stockSymbol))
				stockSymbol = "MSFT";


			//get company profile from API server
			Dictionary<string, object>? companyProfileDictionary = await _finnhubService.GetCompanyProfile(stockSymbol);

			//get stock price quotes from API server
			Dictionary<string, object>? stockQuoteDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);


			//create model object
			StockTrade stockTrade = new StockTrade() { StockSymbol = stockSymbol };

			//load data from finnHubService into model object
			if (companyProfileDictionary != null && stockQuoteDictionary != null)
			{
				stockTrade = new StockTrade() { StockSymbol = companyProfileDictionary["ticker"].ToString(), StockName = companyProfileDictionary["name"].ToString(), Quantity = _tradingOptions.DefaultOrderQuantity ?? 0, Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
			}

			//Send Finnhub token to view
			ViewBag.FinnhubToken = _configuration["FinnhubToken"];

			return View(stockTrade);
		}


		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
		{
			buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

			ModelState.Clear();

			if (!TryValidateModel(buyOrderRequest))
			{
				List<string> errorMessages = new();


                ViewBag.Errors = errorMessages = ModelState.Values
										  .SelectMany(value => value.Errors)
										  .Select(error => error.ErrorMessage)
										  .ToList();

				StockTrade stockTrade = new() 
				{
					StockSymbol = buyOrderRequest.StockSymbol,

					StockName = buyOrderRequest.StockName,

					Price = buyOrderRequest.Price,

					Quantity = buyOrderRequest.Quantity
				};

				return View("Index", stockTrade);
			}

			BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

			return RedirectToAction(nameof(Orders));
		}


		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
		{
			sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

			ModelState.Clear();

			if (!TryValidateModel(sellOrderRequest))
			{
				ViewBag.Errors = ModelState.Values
						  .SelectMany(value => value.Errors)
						  .Select(error => error.ErrorMessage);

				StockTrade stockTrade = new()
				{
					StockName = sellOrderRequest.StockName,

					Price = sellOrderRequest.Price,

					Quantity = sellOrderRequest.Quantity,

					StockSymbol = sellOrderRequest.StockSymbol
				};

				return View("Index", stockTrade);
			}

			SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

			return RedirectToAction(nameof(Orders));
		}


		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Orders()
		{
			List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

			List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();


			Models.Orders orders = new()
			{
				BuyOrders = buyOrderResponses,

				SellOrders = sellOrderResponses
			};

			ViewBag.TradingOptions = _tradingOptions;


			return View(orders);
		}


		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> OrdersPDF()
		{
			Models.Orders orders = new();

			orders.BuyOrders = await _stocksService.GetBuyOrders();

			orders.SellOrders = await _stocksService.GetSellOrders();

			return new ViewAsPdf(orders)
			{
				PageMargins = new Rotativa.AspNetCore.Options.Margins()
				{
					Top = 20,
					Right = 20,
					Bottom = 20,
					Left = 20,
				},

				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
			};
		}
	}
}