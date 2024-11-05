using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Net.Http;
using System.Net.Http.Json;

namespace Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IConfiguration _configuration;

		private readonly IHttpClientFactory _httpClientFactory;


		public FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			configuration = _configuration;

			_httpClientFactory = httpClientFactory;
		}


		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			string profileUrl = 
				$"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}";

			HttpClient httpClient = _httpClientFactory.CreateClient();

			Dictionary<string, object>? responseDictionary;
			
			responseDictionary = await httpClient.GetFromJsonAsync<Dictionary<string, object>?>(profileUrl);

			if(responseDictionary is null)
				throw new InvalidOperationException("No response from server");

			if(responseDictionary.ContainsKey("error"))
				throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

			return responseDictionary;
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			string profileUrl =
				$"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}";

			HttpClient httpClient = _httpClientFactory.CreateClient();

			Dictionary<string, object>? responseDictionary;

			responseDictionary = await httpClient.GetFromJsonAsync<Dictionary<string, object>?>(profileUrl);

			if (responseDictionary is null)
				throw new InvalidOperationException("No response from server");

			if (responseDictionary.ContainsKey("error"))
				throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

			return responseDictionary;
		}
	}
}