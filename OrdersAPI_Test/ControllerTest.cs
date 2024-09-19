using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using OrdersAPI.BusinessRules;
using OrdersAPI.Controllers;
using OrdersAPI.Models;
using OrdersAPI.Repositories;

namespace OrdersAPI_Test
{
	public class ControllerTest
	{
		private Mock<IOrdersBusinessRules> ordersBR;
		private Mock<IOrdersRepository> ordersRepo;
		private OrdersController orderController;
		private List<Order> orders;

		public ControllerTest()
		{
			orders = new List<Order>();
			orders.Add(new Order()
			{
				OrderId = new Guid("{dc805862-5556-4c65-8253-82849743e7c3}"),
				ProductId = new Guid("{0f1f2a9f-e5a4-4cf9-8a18-ab14800e29b6}"),
				ProductName = "Laptop",
				OrderTime = DateTime.UtcNow,
				Quantity = 2,
				UserId = new Guid("0a674ca2-c2b7-4269-ae42-fb7bb70728da")
			});
			orders.Add(new Order()
			{
				OrderId = new Guid("{7102ef84-1489-42ca-a07a-4ad38d33653a}"),
				ProductId = new Guid("{397490ff-e363-4b3c-9170-189b8ccadd14}"),
				ProductName = "Desktop",
				OrderTime = DateTime.UtcNow,
				Quantity = 1,
				UserId = new Guid("0a674ca2-c2b7-4269-ae42-fb7bb70728da")
			});
			ordersBR = new Mock<IOrdersBusinessRules>();
			ordersRepo = new Mock<IOrdersRepository>();
			orderController = new OrdersController(ordersBR.Object, new NullLogger<OrdersController>());
		}

		[Fact]
		public async Task GetOrders()
		{
			ordersRepo.Setup(x => x.GetAllOrders(new CancellationToken())).ReturnsAsync(orders);
			ordersBR.Setup(x => x.GetAllOrders(new CancellationToken())).ReturnsAsync(orders);
			var result = await orderController.Get(new CancellationToken()) as OkObjectResult;

			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(2, (result.Value as List<Order>).Count());
		}

		[Fact]
		public async Task GetOrdersByUser()
		{
			ordersRepo.Setup(x => x.GetAllOrders(new CancellationToken())).ReturnsAsync(orders);
			ordersBR.Setup(x => x.GetUserOrders(new Guid("0a674ca2-c2b7-4269-ae42-fb7bb70728da"), new CancellationToken())).ReturnsAsync(orders);
			var result = await orderController.Get(new Guid("0a674ca2-c2b7-4269-ae42-fb7bb70728da"), new CancellationToken()) as OkObjectResult;

			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(2, (result.Value as List<Order>).Count());
		}

		[Fact]
		public async Task GetOrders_ThrowsException()
		{
			ordersRepo.Setup(x => x.GetAllOrders(new CancellationToken())).Throws(new Exception("Internal server exception"));
			ordersBR.Setup(x => x.GetAllOrders(new CancellationToken())).Throws(new Exception("Internal server exception"));
			var result = await orderController.Get(new CancellationToken()) as ObjectResult;

			Assert.NotNull(result);
			Assert.Equal(500, result.StatusCode);
		}

		[Fact]
		public async Task AddOrder()
		{			
			ordersRepo.Setup(x => x.AddOrder(orders[0])).ReturnsAsync(true);  
			ordersBR.Setup(x => x.AddOrder(orders[0])).ReturnsAsync(true);
			var result = await orderController.Post(orders[0]) as OkObjectResult;

			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(true, result.Value );
		}



	}
}