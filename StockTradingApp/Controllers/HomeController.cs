using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		[Route("Error")]
		public IActionResult Error()
		{
			IExceptionHandlerPathFeature? exceptionHandlerPathFeature = 
				HttpContext.Features.Get<IExceptionHandlerPathFeature>();

			if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
			{
				Error error = new Error() { ErrorMessage = exceptionHandlerPathFeature.Error.Message };

				return View(error);
			}

			else
			{
				Error error = new Error() { ErrorMessage = "Error encountered" };

				return View(error);
			}
		}
	}
}