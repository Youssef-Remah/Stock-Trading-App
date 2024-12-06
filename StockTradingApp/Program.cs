using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;
using StockTradingApp;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();

builder.Services.Configure<TradingOptions>(
	builder.Configuration.GetSection("TradingOptions")
);

builder.Services.AddTransient<IFinnhubService, FinnhubService>();

builder.Services.AddTransient<IStocksService, StocksService>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<StockMarketDbContext>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

var app = builder.Build();


//Middleware Pipline
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();