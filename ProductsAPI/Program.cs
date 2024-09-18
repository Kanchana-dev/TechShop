
using ProductsAPI.BusinessRules;
using ProductsAPI.Middlerware;
using ProductsAPI.Repositories;
using Serilog;

namespace ProductsAPI
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
			builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
			builder.Services.AddScoped<IProductsBusinessRules, ProductsBusinessRules>();

			//Middlerware - GlobalExceptionHandler
			builder.Services.AddExceptionHandler<APIGlobalException>();
			builder.Services.AddProblemDetails();

			//SQLite BD Context Configuration
			//var connectionString = builder.Configuration.GetConnectionString("ProductDB");
			//builder.Services.AddDbContextFactory<ProductDataContext>(options => options.UseSqlite(connectionString));

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
