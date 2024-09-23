using Newtonsoft.Json;
using ProductsAPI.Models;

namespace ProductsAPI.Repositories
{
	public class ProductsRepository: IProductsRepository
	{
		//***********************************************Information***************************************************************
		//DBContext class can be Dependency Injected and used to work with EntityFramework, or to call ADO.NET methods to wotk with 
		//MySQL or other no SQL database context objects like SQLite, MongDB Documents. 
		//For simplicty of the POC, data is saved and retrieved from a JSON file.
		//*************************************************************************************************************************

		private readonly ILogger<ProductsRepository> _logger;
		private static readonly string productsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "products.json");
		private static readonly string quantityFilePath = Path.Combine(Directory.GetCurrentDirectory(), "quantity.json");

		public ProductsRepository(ILogger<ProductsRepository> logger) 
		{
			_logger = logger;
		}

		public async Task<List<Product>> GetAllProducts()
		{
			try
			{	
				_logger.LogInformation("In ProductsRepository");

				using (StreamReader r = new StreamReader(productsFilePath))
				{
					string productsJson = r.ReadToEnd();
					
					var items = JsonConvert.DeserializeObject<ProductList>(productsJson);
					var products =  items?.Products;
					if (products != null && products.Count() > 0)
					{
						_logger.LogInformation("Successfully got all Products");
						return products;
					}
					else
					{ 
						_logger.LogInformation("UnSuccessfully getting Products");
					}
				}

				return null;
			}
			catch(Exception ex) 
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return null;
			}
		}

		//This would typically be a Master data table in RDBMS like a Quantity.dbo and respective C# Model - Quantity
		//or a MongoDB Document/Json file in no SQL DBs.
		public async Task<List<int>> GetQuantity()
		{
			try
			{
				_logger.LogInformation("In ProductsRepository");
				using (StreamReader r = new StreamReader(quantityFilePath))
				{
					string quantityJson = r.ReadToEnd();

					var items = JsonConvert.DeserializeObject<QuantityList>(quantityJson);
					var quantity = items?.Quantity;
					if (quantity != null && quantity.Count() > 0)
					{
						_logger.LogInformation("Successfully got Quantity for Products");
						return quantity;
					}
					else
					{
						_logger.LogInformation("UnSuccessfully getting Quantity");
					}
				}

				return null;
			}
			catch(Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return null;
			}
		}

	}
}
