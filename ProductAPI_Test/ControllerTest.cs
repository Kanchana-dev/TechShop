using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ProductsAPI.BusinessRules;
using ProductsAPI.Controllers;
using ProductsAPI.Models;
using ProductsAPI.Repositories;

namespace ProductAPI_Test
{
	public class ControllerTest
	{
		private Mock<IProductsBusinessRules> productBR;
		private Mock<IProductsRepository> productRepo;
		private ProductsController productController;
		private List<Product> products;
		private List<int> quantity;

		public ControllerTest()
		{
			products = new List<Product>();
			products.Add(new Product() { Id = new Guid("{801a4730-2207-4a82-9c14-b3d58d07d41e}"), Name = "Laptop" });
			products.Add(new Product() { Id = new Guid("{f35da555-3f49-4520-9084-cf299a79ee1b}"), Name = "Desktop" });

			quantity = new List<int>{ 1,2,3};
			productBR = new Mock<IProductsBusinessRules>();
			productRepo = new Mock<IProductsRepository>();
			productController = new ProductsController(productBR.Object, new NullLogger<ProductsController>());
		}


		[Fact]
		public async Task GetProducts()
		{
			productRepo.Setup(x=>x.GetAllProducts()).ReturnsAsync(products);
			productBR.Setup(x => x.GetAllProducts()).ReturnsAsync(products);
			var result = await productController.Get() as OkObjectResult;		

			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(2, (result.Value as List<Product>).Count());
		}

		[Fact]
		public async Task GetQuantity()
		{
			productRepo.Setup(x => x.GetQuantity()).ReturnsAsync(quantity);
			productBR.Setup(x => x.GetQuantity()).ReturnsAsync(quantity);
			var result = await productController.GetQuantity() as OkObjectResult;

			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(3, (result.Value as List<int>).Count());
		}

		[Fact]
		public async Task GetProducts_ThrowsException()
		{
			productRepo.Setup(x => x.GetAllProducts()).Throws(new Exception("Internal server exception"));
			productBR.Setup(x => x.GetAllProducts()).Throws(new Exception("Internal server exception"));

			var result = await productController.Get() as ObjectResult;

			Assert.NotNull(result);
			Assert.Equal(500, result.StatusCode);
		}

		[Fact]
		public async Task GetQuantity_ThrowsException()
		{
			productRepo.Setup(x => x.GetQuantity()).Throws(new Exception("Internal server exception"));
			productBR.Setup(x => x.GetQuantity()).Throws(new Exception("Internal server exception"));

			var result = await productController.GetQuantity() as ObjectResult;

			Assert.NotNull(result);
			Assert.Equal(500, result.StatusCode);
		}

	}
}