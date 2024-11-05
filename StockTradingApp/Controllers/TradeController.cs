using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{

	[Route("[controller]")]
	public class TradeController : Controller
	{
		private readonly IConfiguration _configuration;

		private readonly TradingOptions _tradingOptions;

		private readonly IFinnhubService _finnhubService;


		public TradeController(
			IConfiguration configuration,

			IOptions<TradingOptions> tradingOptions,

			IFinnhubService finnhubService
		)
		{
			configuration = _configuration;

			_tradingOptions = tradingOptions.Value;

			_finnhubService = finnhubService;
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
	}
}