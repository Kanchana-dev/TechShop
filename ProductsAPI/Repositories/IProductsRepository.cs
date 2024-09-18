using ProductsAPI.Models;

namespace ProductsAPI.Repositories
{
	public interface IProductsRepository
	{
		Task<List<Product>> GetAllProducts();
		Task<List<int>> GetQuantity();
	}
}
