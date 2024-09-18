
using OrdersAPI.BusinessRules;
using OrdersAPI.Middleware;
using OrdersAPI.Repositories;
using Serilog;

namespace OrdersAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			//Serilog Configuration
			var seriloglogger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Configuration)
				.Enrich.FromLogContext().CreateLogger();
			builder.Logging.ClearProviders();
			builder.Logging.AddSerilog(seriloglogger);

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Dependency Injection			
			builder.Services.AddScoped<IOrdersBusinessRules, OrdersBusinessRules>();
			builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

			//Middlerware - GlobalExceptionHandler
			builder.Services.AddExceptionHandler<OrdersAPIGlobalExceptionHandler>();
			builder.Services.AddProblemDetails();

			var app = builder.Build();

			app.UseExceptionHandler();
			app.UseStatusCodePages();
			app.UseCors(x => x
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();
			app.UseExceptionHandler();

			app.MapControllers();

			app.Run();
		}
	}
}
