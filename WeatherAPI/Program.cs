using Serilog;
using WeatherAPI.BusinessRules;
using WeatherAPI.Repositories;
using WeatherAPI.Service;

namespace OrdersAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			//Serilog Configuration
			var seriloglogger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Configuration)
				.Enrich.FromLogContext().CreateLogger();
			builder.Logging.ClearProviders();
			builder.Logging.AddSerilog(seriloglogger);

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			// Dependency Injection			
			builder.Services.AddScoped<IWeatherBR, WeatherBR>();
			builder.Services.AddScoped<IWeatherRepo, WeatherRepo>();

			builder.Services.AddHttpClient<MeteoClient>(client =>
			{
				client.Timeout = TimeSpan.FromSeconds(10);
			});

			builder.Services.AddSingleton<WeatherStorage>();			
			builder.Services.AddSingleton<MeteoClient>();

			const string policy = "defaultPolicy";
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(policy,
								  p =>
								  {
									  p.AllowAnyHeader();
									  p.AllowAnyMethod();
									  p.AllowAnyHeader();
									  p.AllowAnyOrigin();
								  });
			});

			var app = builder.Build();
			app.UseStatusCodePages();
			app.UseCors(x => x
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

			app.MapControllers();

			app.Run();
		}
	}
}
