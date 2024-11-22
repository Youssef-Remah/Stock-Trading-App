using ServiceContracts.DTO;

namespace StockTradingApp.Models
{
	/// <summary>
	/// Represents the view model class to supply list of buy and sell orders to the Trade/Orders view
	/// </summary>
	public class Orders
	{
		public List<BuyOrderResponse>? BuyOrders { get; set; } = new();

		public List<SellOrderResponse>? SellOrders { get; set; } = new();
	}
}