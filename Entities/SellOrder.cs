using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Represents a sell order to sell the stocks
    /// </summary>
    public class SellOrder
    {
        /// <summary>
        /// The unique ID of the sell order
        /// </summary>
        [Display(Name = "Order ID")]
        [Key]
        public Guid SellOrderID { get; set; }


        /// <summary>
        /// The unique symbol of the stock
        /// </summary>
        [Display(Name = "Stock Symbol")]
        [Required(ErrorMessage = "{0} cannot be blank!")]
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
        [Range(1, 100000, ErrorMessage = "You can buy maximum of {2} shares in single order. Minimum is {1}.")]
        public uint Quantity { get; set; }


        /// <summary>
        /// The price of each stock (share)
        /// </summary>
        [Display(Name = "price")]
        [Range(1, 100000, ErrorMessage = "The maximum {0} of stock is {2}. Minimum is {1}.")]
        public double Price { get; set; }
    }
}