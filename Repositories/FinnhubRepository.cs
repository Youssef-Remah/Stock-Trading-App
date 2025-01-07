using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using Serilog;
using System.Net.Http.Json;

namespace Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {

        private readonly IConfiguration _configuration;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<FinnhubRepository> _logger;

        private readonly IDiagnosticContext _diagnosticContext;


        public FinnhubRepository
        (
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<FinnhubRepository> logger,
            IDiagnosticContext diagnosticContext
        )
        {
            _configuration = configuration;

            _httpClientFactory = httpClientFactory;

            _logger = logger;

            _diagnosticContext = diagnosticContext;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            _logger.LogInformation("In {ClassName}.{MethodName}", nameof(FinnhubRepository), nameof(GetCompanyProfile));

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
            _logger.LogInformation("In {ClassName}.{MethodName}", nameof(FinnhubRepository), nameof(GetStockPriceQuote));

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
            _logger.LogInformation("In {ClassName}.{MethodName}", nameof(FinnhubRepository), nameof(GetStocks));

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
            _logger.LogInformation("In {ClassName}.{MethodName}", nameof(FinnhubRepository), nameof(SearchStocks));

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