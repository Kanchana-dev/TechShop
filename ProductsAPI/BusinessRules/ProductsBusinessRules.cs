using ProductsAPI.Models;
using ProductsAPI.Repositories;

namespace ProductsAPI.BusinessRules
{
	public class ProductsBusinessRules : IProductsBusinessRules
	{
		private readonly IProductsRepository _productsRepo;
		private readonly ILogger<ProductsBusinessRules> _logger;
		public ProductsBusinessRules(IProductsRepository productsRepo, ILogger<ProductsBusinessRules> logger)
		{
			_productsRepo = productsRepo;
			_logger = logger;
		}
		public async Task<List<Product>> GetAllProducts()
		{
			_logger.LogInformation("In ProductsBusinessRules");
			var products = _productsRepo.GetAllProducts().Result;
			if (products != null && products.Count > 0)
			{
				_logger.LogInformation("Leaving ProductsBusinessRules");

				return products;
			}

			return new List<Product>();
		}

		public async Task<List<int>> GetQuantity()
		{
			_logger.LogInformation("In ProductsBusinessRules");

			var quantity = _productsRepo.GetQuantity().Result;
			if (quantity != null )
			{
				_logger.LogInformation("Leaving ProductsBusinessRules");
				return quantity;
			}

			return null;
		}
	}
}
