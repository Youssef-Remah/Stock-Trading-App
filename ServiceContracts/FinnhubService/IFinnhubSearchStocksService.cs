namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubSearchStocksService
    {
        /// <summary>
        /// Returns list of matching stocks based on the given stock symbol
        /// </summary>
        /// <param name="stockSymbolToSearch">Stock symbol to search</param>
        /// <returns>List of matching stocks</returns>
        public Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}