using Entities;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Services;
using StockTradingApp;
using StockTradingApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog(
	(HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
	{
		loggerConfiguration.ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
                           .ReadFrom.Services(services); //read out current app's services and make them available to serilog
    }
);

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

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponsePropertiesAndHeaders;
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseMiddleware<ExceptionHandlingMiddleware>();
}


if (!builder.Environment.IsEnvironment("Test"))
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
}

//Middleware Pipline
app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();

public partial class Program {}