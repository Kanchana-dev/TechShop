namespace ProductsAPI.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}

	public class ProductList
	{
		public List<Product> Products { get; set; }
	}
}
