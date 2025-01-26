using Exceptions;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IFinnhubRepository _finnhubRepository;


		public FinnhubService(IFinnhubRepository finnhubRepository)
		{
			_finnhubRepository = finnhubRepository;
		}


		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			try
			{
				Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);

				return responseDictionary;
			}

			catch (Exception ex)
			{
				throw new FinnhubException("Unable to connect to finnhub", ex);
			}
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			try
			{
				Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);

				return responseDictionary;
			}

			catch (Exception ex)
			{
				throw new FinnhubException("Unable to connect to finnhub", ex);
			}
		}

		public async Task<List<Dictionary<string, string>>?> GetStocks()
		{
			try
			{
				List<Dictionary<string, string>>? responseList = await _finnhubRepository.GetStocks();

				return responseList;
			}

			catch (Exception ex)
			{
				throw new FinnhubException("Unable to connect to finnhub", ex);
			}
		}

		public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
		{
			try
			{
				Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);

				return responseDictionary;
			}

			catch (Exception ex)
			{
				throw new FinnhubException("Unable to connect to finnhub", ex);
			}
		}
    }
}