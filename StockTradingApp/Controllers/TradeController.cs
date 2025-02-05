using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts.DTO;
using ServiceContracts.FinnhubService;
using ServiceContracts.StocksService;
using StockTradingApp.Filters.ActionFilters;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{

	[Route("[controller]")]
	public class TradeController : Controller
	{
		private readonly IConfiguration _configuration;

		private readonly TradingOptions _tradingOptions;

		private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;

		private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;

		private readonly IBuyOrdersService _stocksBuyOrdersService;

		private readonly ISellOrdersService _stocksSellOrdersService;

		private readonly ILogger<TradeController> _logger;


		public TradeController(
			IConfiguration configuration,

			IOptions<TradingOptions> tradingOptions,

            IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService,

            IFinnhubCompanyProfileService finnhubCompanyProfileService,

            IBuyOrdersService stocksBuyOrdersService,

            ISellOrdersService stocksSellOrdersService,

            ILogger<TradeController> logger
        )
		{
			_configuration = configuration;

			_tradingOptions = tradingOptions.Value;

            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;

            _finnhubCompanyProfileService = finnhubCompanyProfileService;

			_stocksBuyOrdersService = stocksBuyOrdersService;

			_stocksSellOrdersService = stocksSellOrdersService;

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
			Dictionary<string, object>? companyProfileDictionary = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);

			//get stock price quotes from API server
			Dictionary<string, object>? stockQuoteDictionary = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);


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
		[TypeFilter(typeof(CreateOrderActionFilter))]
		public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
		{
			BuyOrderResponse buyOrderResponse = await _stocksBuyOrdersService.CreateBuyOrder(orderRequest);

			return RedirectToAction(nameof(Orders));
		}


		[HttpPost]
		[Route("[action]")]
		[TypeFilter(typeof(CreateOrderActionFilter))]
		public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
		{
			SellOrderResponse sellOrderResponse = await _stocksSellOrdersService.CreateSellOrder(orderRequest);

			return RedirectToAction(nameof(Orders));
		}


		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Orders()
		{
			List<BuyOrderResponse> buyOrderResponses = await _stocksBuyOrdersService.GetBuyOrders();

			List<SellOrderResponse> sellOrderResponses = await _stocksSellOrdersService.GetSellOrders();


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

			orders.BuyOrders = await _stocksBuyOrdersService.GetBuyOrders();

			orders.SellOrders = await _stocksSellOrdersService.GetSellOrders();

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