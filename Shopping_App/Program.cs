using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Shopping_App.Services;
using Shopping_App.Storage;

namespace Shopping_App
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

            //Serilog Configuration
            var seriloglogger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext().CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(seriloglogger);

            //Configure all API Calls with HTTPClient
            builder.Services.AddHttpClient("authService")
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["AuthAPIUrl"] ?? ""));

			builder.Services.AddHttpClient("ordersService")
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["OrdersAPIUrl"] ?? ""));

			builder.Services.AddScoped<LocalStorage>();
			builder.Services.AddScoped<AuthService>();
			builder.Services.AddScoped<AppHttpService>();
			
			builder.Services.AddHttpClient("productsService")
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ProductsAPIUrl"] ?? ""));			
		
			builder.Services.AddScoped<OrdersService>();
			builder.Services.AddScoped<ProductsService>();

			await builder.Build().RunAsync();

		}

	}
}
