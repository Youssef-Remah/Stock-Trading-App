using Entities;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.StocksService;
using Services;
using Services.StocksService;
using StockTradingApp.Middleware;

namespace StockTradingApp.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<TradingOptions>(
                configuration.GetSection("TradingOptions")
            );

            services.AddTransient<IFinnhubService, FinnhubService>();

            services.AddTransient<ISellOrdersService, StocksSellOrdersService>();

            services.AddTransient<IBuyOrdersService, StocksBuyOrdersService>();

            services.AddTransient<IFinnhubRepository, FinnhubRepository>();

            services.AddTransient<IStocksRepository, StocksRepository>();

            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddHttpClient();

            services.AddDbContext<StockMarketDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            return services;
        }
    }
}