namespace ServiceContracts
{
	/// <summary>
	/// Represents a service that makes HTTP requests to finnhub.io
	/// </summary>
	public interface IFinnhubService
	{
		/// <summary>
		/// Returns company details such as company name, company logo, country
		/// </summary>
		/// <param name="stockSymbol">Stock symbol to search for</param>
		/// <returns>Returns a dictionary that contains company details</returns>
		public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);


		/// <summary>
		/// Returns stock price details such as current price, change in price, high/low price of the day
		/// </summary>
		/// <param name="stockSymbol">Stock symbol to search for</param>
		/// <returns>Returns a dictionary that contains stock price details</returns>
		public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);


        /// <summary>
        /// Returns list of all stocks supported by an exchange (default: US)
        /// </summary>
        /// <returns>List of stocks</returns>
        public Task<List<Dictionary<string, string>>?> GetStocks();


        /// <summary>
        /// Returns list of matching stocks based on the given stock symbol
        /// </summary>
        /// <param name="stockSymbolToSearch">Stock symbol to search</param>
        /// <returns>List of matching stocks</returns>
        public Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}