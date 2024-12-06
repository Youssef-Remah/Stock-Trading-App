using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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


		public TradeController(
			IConfiguration configuration,

			IOptions<TradingOptions> tradingOptions,

			IFinnhubService finnhubService,

			IStocksService stocksService
		)
		{
			_configuration = configuration;

			_tradingOptions = tradingOptions.Value;

			_finnhubService = finnhubService;

			_stocksService = stocksService;
		}


		[HttpGet]
		[Route("/")]
		[Route("[action]")]
		[Route("~/[controller]")]
		public async Task<IActionResult> Index()
		{
			//set the default stock symbol if doesn't exist
			if(string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
				_tradingOptions.DefaultStockSymbol = "MSFT";

			//get company profile from API server
			Dictionary<string, object>? companyProfileDictionary;

			companyProfileDictionary = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);


			//get stock price quotes from API server
			Dictionary<string, object>? stockQuoteDictionary;

			stockQuoteDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);


			StockTrade stockTrade = new() { StockSymbol = _tradingOptions.DefaultStockSymbol };

			if (companyProfileDictionary is not null && stockQuoteDictionary is not null)
			{
				stockTrade.StockSymbol = companyProfileDictionary["ticker"].ToString();

				stockTrade.StockName = companyProfileDictionary["name"].ToString();

				stockTrade.Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString());
			}

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