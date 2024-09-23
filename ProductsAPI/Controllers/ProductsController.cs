using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.BusinessRules;

namespace ProductsAPI.Controllers
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductsBusinessRules _productsBusinessRules;
		private readonly ILogger<ProductsController> _logger;

		public ProductsController(IProductsBusinessRules productsBusinessRules, ILogger<ProductsController> logger)
		{
			_productsBusinessRules = productsBusinessRules;
			_logger = logger;
		}

		[HttpGet]		
		public async Task<IActionResult> Get()
		{
			_logger.LogInformation("Getting All Products");

			try
			{
				return Ok(await _productsBusinessRules.GetAllProducts());
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet]
		[Route("quantity")]
		public async Task<IActionResult> GetQuantity()
		{
			_logger.LogInformation("Getting Quantity");

			try
			{			
				return Ok(await _productsBusinessRules.GetQuantity());
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return StatusCode(500, ex.Message);
			}
		}
	}
}
