using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            //Set Environment for Testing
            builder.UseEnvironment("Test");

            //Configure Services before building the application
            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.SingleOrDefault(service 
                    => service.ServiceType == typeof(DbContextOptions<StockMarketDbContext>));

                if (serviceDescriptor is not null)
                {
                    //Remove the Service of the StockMarketDbContext that uses the SQL Server Database
                    services.Remove(serviceDescriptor);
                }

                services.AddDbContext<StockMarketDbContext>(options =>
                {
                    //Register the StockMarketDbContext service again, but with in-memory collection as the data store
                    options.UseInMemoryDatabase("DatbaseForTesting");
                });
            });
        }

    }
}