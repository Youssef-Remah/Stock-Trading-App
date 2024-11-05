namespace StockTradingApp.Models
{
	public class StockTrade
	{
		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public double Price { get; set; } = 0;

		public uint Quantity { get; set; } = 0;
	}
}