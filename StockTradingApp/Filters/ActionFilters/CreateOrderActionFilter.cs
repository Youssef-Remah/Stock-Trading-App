using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using StockTradingApp.Controllers;
using StockTradingApp.Models;

namespace StockTradingApp.Filters.ActionFilters
{
    /// <summary>
    /// An Action Filter that applies model validation to BuyOrder() and SellOrder() methods in TradeController
    /// </summary>
    public class CreateOrderActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is TradeController tradeController)
            {
                var orderRequest = context.ActionArguments["orderRequest"] as IOrderRequest;

                if (orderRequest != null)
                {
                    orderRequest.DateAndTimeOfOrder = DateTime.Now;

                    tradeController.ModelState.Clear();

                    if (!tradeController.TryValidateModel(orderRequest))
                    {
                        tradeController.ViewBag.Errors =
                            tradeController.ModelState
                                           .Values
                                           .SelectMany(value => value.Errors)
                                           .Select(error => error.ErrorMessage)
                                           .ToList();

                        StockTrade stockTrade = new()
                        {
                            StockSymbol = orderRequest.StockSymbol,

                            StockName = orderRequest.StockName,

                            Price = orderRequest.Price,

                            Quantity = orderRequest.Quantity
                        };

                        //short-circuits the subsequent action filters & action method
                        context.Result = tradeController.View(nameof(tradeController.Index), stockTrade);

                        return;
                    }
                }
            }

            await next();
        }
    }
}