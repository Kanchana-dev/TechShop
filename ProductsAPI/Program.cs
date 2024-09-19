
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsAPI.BusinessRules;
using ProductsAPI.Middlerware;
using ProductsAPI.Repositories;
using Serilog;
using System.Text;

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
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter 'Bearer [jwt]'",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				var scheme = new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, Array.Empty<string>() } });
			});

			// Dependency Injection			
			builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
			builder.Services.AddScoped<IProductsBusinessRules, ProductsBusinessRules>();

			//Middlerware - GlobalExceptionHandler
			builder.Services.AddExceptionHandler<APIGlobalException>();
			builder.Services.AddProblemDetails();

			//SQLite BD Context Configuration
			//var connectionString = builder.Configuration.GetConnectionString("ProductDB");
			//builder.Services.AddDbContextFactory<ProductDataContext>(options => options.UseSqlite(connectionString));

			//JWTToken Autherization
			var secret = builder.Configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("Secret not configured");

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{				
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
					ClockSkew = new TimeSpan(0, 0, 5)
				};				
			});

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
			
			app.UseAuthentication();
			
			app.UseAuthorization();

			app.UseExceptionHandler();
			app.MapControllers();

			app.Run();
		}
	}
}
