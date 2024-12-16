using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System.Net.Http.Json;

namespace Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {

        private readonly IConfiguration _configuration;

        private readonly IHttpClientFactory _httpClientFactory;

        public FinnhubRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;

            _httpClientFactory = httpClientFactory;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            string profileUrl =
                $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}";

            HttpClient httpClient = _httpClientFactory.CreateClient();

            Dictionary<string, object>? responseDictionary;

            responseDictionary = await httpClient.GetFromJsonAsync<Dictionary<string, object>?>(profileUrl);

            if (responseDictionary is null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
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

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            string uri =
                $"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["FinnhubToken"]}";

            HttpClient httpClient = _httpClientFactory.CreateClient();

            List<Dictionary<string, string>>? responseDictionary;

            responseDictionary = await httpClient.GetFromJsonAsync<List<Dictionary<string, string>>?>(uri);

            if(responseDictionary is null)
                throw new InvalidOperationException("No response from server");

            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            string uri =
                $"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["FinnhubToken"]}";

            HttpClient httpClient = _httpClientFactory.CreateClient();

            Dictionary<string, object>? responseDictionary;

            responseDictionary = await httpClient.GetFromJsonAsync<Dictionary<string, object>?>(uri);

            if (responseDictionary is null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }
    }
}