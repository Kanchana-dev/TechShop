using Shopping_App.Models;

namespace Shopping_App.Services
{
	public interface IProductsService
	{
		Task<List<Product>> GetAllProducts();
	}
}
