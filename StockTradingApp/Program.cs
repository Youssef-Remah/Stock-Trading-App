using ServiceContracts;
using Services;
using StockTradingApp;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();

builder.Services.Configure<TradingOptions>(
	builder.Configuration.GetSection("TradingOptions")
);

builder.Services.AddSingleton<IFinnhubService, FinnhubService>();

builder.Services.AddSingleton<IStocksService, StocksService>();

builder.Services.AddHttpClient();


var app = builder.Build();


//Middleware Pipline
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();