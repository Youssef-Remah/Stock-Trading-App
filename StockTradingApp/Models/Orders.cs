using ServiceContracts.DTO;

namespace StockTradingApp.Models
{
	public class Orders
	{
		List<BuyOrderResponse>? BuyOrders { get; set; }

		List<SellOrderResponse>? SellOrders { get; set; }
	}
}