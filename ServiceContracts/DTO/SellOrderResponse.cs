using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that represents a sell order - that can be used as a return type of Stocks Service
    /// </summary>
    public class SellOrderResponse
    {
        /// <summary>
        /// The unique ID of the sell order
        /// </summary>
        [Display(Name = "Order ID")]
        public Guid SellOrderID { get; set; }


        /// <summary>
        /// The unique symbol of the stock
        /// </summary>
        [Display(Name = "Stock Symbol")]
        public string? StockSymbol { get; set; }


        /// <summary>
        /// The company name of the stock
        /// </summary>
        [Display(Name = "Stock Name")]
        [Required(ErrorMessage = "{0} cannot be blank!")]
        public string? StockName { get; set; }


        /// <summary>
        /// Date and Time of order when it is placed by the user
        /// </summary>
        [Display(Name = "Order Date")]
        public DateTime DateAndTimeOfOrder { get; set; }


        /// <summary>
        /// The number of stocks (shares) to sell
        /// </summary>
        [Display(Name = "Quantity")]
        public uint Quantity { get; set; }


        /// <summary>
        /// The price of each stock (share)
        /// </summary>
        [Display(Name = "price")]
        public double Price { get; set; }


        public double TradeAmount { get; set; }


        /// <summary>
        /// Checks if two objects of SellOrderResponse types match
        /// </summary>
        /// <param name="obj">Other object of SellOrderResponse class to match</param>
        /// <returns>True or False, determines whether current object and obj (parameter) match</returns>
        public override bool Equals(object? obj)
        {
            if(obj is null || obj is not SellOrderResponse)
                return false;

            SellOrderResponse sellOrderResponse = (SellOrderResponse)obj;

            return SellOrderID == sellOrderResponse.SellOrderID && StockSymbol == sellOrderResponse.StockSymbol &&
                Price == sellOrderResponse.Price && StockName == sellOrderResponse.StockName &&
                DateAndTimeOfOrder == sellOrderResponse.DateAndTimeOfOrder && Quantity == sellOrderResponse.Quantity;
        }


        /// <summary>
        /// Gets the hash code that represents unique stock id of the current object
        /// </summary>
        /// <returns>A unique int value</returns>
        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

        /// <summary>
        /// Converts the current object into string that includes the values of all properties
        /// </summary>
        /// <returns>A string with values of all properties of current object</returns>
        public override string ToString()
        {
            return $"Sell Order ID: {SellOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, " +
                $"Date and Time of Sell Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, " +
                $"Quantity: {Quantity}, Sell Price: {Price}, Trade Amount: {TradeAmount}";
        }
    }

    public static class SellOrderExtensions
    {

        /// <summary>
        /// An extension method to convert an object of SellOrder type into SellOrderResponse type
        /// </summary>
        /// <param name="sellOrder">The SellOrder object to convert</param>
        /// <returns>Returns the converted SellOrderResponse object</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new()
            {
                SellOrderID = sellOrder.SellOrderID,

                StockSymbol = sellOrder.StockSymbol,

                StockName = sellOrder.StockName,

                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,

                Quantity = sellOrder.Quantity,

                Price = sellOrder.Price,

                TradeAmount = sellOrder.Price * sellOrder.Quantity
            };
        }
    }
}