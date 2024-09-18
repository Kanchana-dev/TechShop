using ProductsAPI.Models;

namespace ProductsAPI.BusinessRules
{
	public interface IProductsBusinessRules
	{
		Task<List<Product>> GetAllProducts();
		Task<List<int>> GetQuantity();

	}
}
