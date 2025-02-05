namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubStockPriceQuoteService
    {
        /// <summary>
        /// Returns stock price details such as current price, change in price, high/low price of the day
        /// </summary>
        /// <param name="stockSymbol">Stock symbol to search for</param>
        /// <returns>Returns a dictionary that contains stock price details</returns>
        public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}