using Serilog;
using StockTradingApp.Middleware;
using StockTradingApp.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog(
	(HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
	{
		loggerConfiguration.ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
                           .ReadFrom.Services(services); //read out current app's services and make them available to serilog
    }
);

//Configure Services
builder.Services.ConfigureServices(builder.Configuration);

WebApplication app = builder.Build();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");

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