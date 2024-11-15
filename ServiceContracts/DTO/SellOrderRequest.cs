using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that represents a sell order - that can be used while inserting or updating
    /// </summary>
    public class SellOrderRequest : IValidatableObject
    {
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


        public SellOrder ToSellOrder()
        {
            return new()
            {
                StockName = StockName,

                StockSymbol = StockSymbol,

                DateAndTimeOfOrder = DateAndTimeOfOrder,

                Quantity = Quantity,

                Price = Price
            };
        }


        /// <summary>
        /// Model class-level validation using IValidatableObject
        /// </summary>
        /// <param name="validationContext">ValidationContext to validate</param>
        /// <returns>Returns validation errors as ValidationResult</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new();

            if (DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01")) 
            {
                results.Add(new ValidationResult("Date of the order should not be older than Jan 01, 2000."));
            }

            return results;
        }
    }
}