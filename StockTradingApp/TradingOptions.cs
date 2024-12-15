namespace StockTradingApp
{
	/// <summary>
	/// Represents the options pattern for "StockPrice" configuration
	/// </summary>
	public class TradingOptions
	{
		public string? DefaultStockSymbol { get; set; }

        public uint? DefaultOrderQuantity { get; set; }

        public string? Top25PopularStocks { get; set; }
    }
}