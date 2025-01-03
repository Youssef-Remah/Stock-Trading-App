using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
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

builder.Services.AddTransient<IFinnhubRepository, FinnhubRepository>();

builder.Services.AddTransient<IStocksRepository, StocksRepository>();
	
builder.Services.AddHttpClient();

builder.Services.AddDbContext<StockMarketDbContext>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (!builder.Environment.IsEnvironment("Test"))
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
}

//Middleware Pipline
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();

public partial class Program {}