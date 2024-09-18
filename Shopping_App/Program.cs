using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shopping_App.Services;

namespace Shopping_App
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			//Configure all API Calls with HTTPClient
			builder.Services.AddHttpClient("ordersService")
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["OrdersAPIUrl"] ?? ""));

			builder.Services.AddHttpClient("productsService")
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ProductsAPIUrl"] ?? ""));

			//Dependency Inject services
			builder.Services.AddScoped<OrdersService>();
			builder.Services.AddScoped<ProductsService>();
			
			

			await builder.Build().RunAsync();
		}

	}
}
