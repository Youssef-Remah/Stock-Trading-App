using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that represents a buy order to purchase the stocks - that can be used as return type of Stocks service
    /// </summary>
    public class BuyOrderResponse
    {
        /// <summary>
        /// The unique ID of the buy order
        /// </summary>
        [Display(Name = "Order ID")]
        public Guid BuyOrderID { get; set; }


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
        /// The number of stocks (shares) to buy
        /// </summary>
        [Display(Name = "Quantity")]
        public uint Quantity { get; set; }


        /// <summary>
        /// The price of each stock (shares)
        /// </summary>
        [Display(Name = "price")]
        public double Price { get; set; }


        public double TradeAmount { get; set; }


        /// <summary>
        /// Checks if two objects of BuyOrderResponse types match
        /// </summary>
        /// <param name="obj">Other object of BuyOrderResponse class to match</param>
        /// <returns>True or False, determines whether current object and obj (parameter) match</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not BuyOrderResponse)
                return false;

            BuyOrderResponse buyOrderResponse = (BuyOrderResponse)obj;

            return BuyOrderID == buyOrderResponse.BuyOrderID && StockSymbol == buyOrderResponse.StockSymbol &&
                StockName == buyOrderResponse.StockName && DateAndTimeOfOrder == buyOrderResponse.DateAndTimeOfOrder &&
                Price == buyOrderResponse.Price;
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
            return $"Buy Order ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, " +
                $"Date and Time of Buy Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, " +
                $"Quantity: {Quantity}, Buy Price: {Price}, Trade Amount: {TradeAmount}";
        }
    }


    public static class BuyOrderExtensions 
    {
        /// <summary>
        /// An extension method to convert an object of BuyOrder type into BuyOrderResponse type
        /// </summary>
        /// <param name="buyOrder">The BuyOrder object to convert</param>
        /// <returns>Returns the converted BuyOrderResponse object</returns>
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new()
            {
                BuyOrderID = buyOrder.BuyOrderID,

                StockSymbol = buyOrder.StockSymbol,

                StockName = buyOrder.StockName,
                
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,

                Quantity = buyOrder.Quantity,

                Price = buyOrder.Price,

                TradeAmount = buyOrder.Price * buyOrder.Quantity
            };
        }
    }
}