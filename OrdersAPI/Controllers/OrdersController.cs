using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrdersAPI.BusinessRules;
using OrdersAPI.Models;

namespace OrdersAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrdersBusinessRules _ordersBusinessRules;
		private readonly ILogger<OrdersController> _logger;
		
		public OrdersController(IOrdersBusinessRules ordersBusinessRules, ILogger<OrdersController> logger)
		{
			_ordersBusinessRules = ordersBusinessRules;
			_logger = logger;
		}

		[HttpGet]
		[Route("getallorders")]
		public async Task<IActionResult> Get(CancellationToken ct = default)
		{
			_logger.LogInformation("Getting All Orders");
			
			try
			{
				return Ok(_ordersBusinessRules.GetAllOrders(ct).Result);
			}
			catch (OperationCanceledException cte)
			{
				_logger.LogError(cte.Message, cte.InnerException, cte.Source);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.StackTrace, ex.InnerException);
				return StatusCode(500, ex.Message);
			}			
		}


		// GET api/<OrdersController>/5
		[HttpGet("getallorders/{id}")]
		public async Task<IActionResult> Get(int id, CancellationToken ct=default)
		{
            _logger.LogInformation($"Get Orders by UserId - {id}");

            try
            {		
				return Ok(_ordersBusinessRules.GetUserOrders(id, ct).Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex.InnerException);
                return StatusCode(500, ex.Message);
            }
        }

		// POST api/<OrdersController>
		[HttpPost]
		[Route("addorder")]
		public async Task<IActionResult> Post([FromBody] Order order, CancellationToken ct = default)
		{
			_logger.LogInformation($"Adding new Order : {JsonConvert.SerializeObject(order)} ");
			if (order == null)
			{
				_logger.LogInformation("Order is null");
				return BadRequest();
			}

			try
			{
				return Ok(_ordersBusinessRules.AddOrder(order).Result);
			}
			catch (TaskCanceledException cte)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.StackTrace, ex.InnerException);
				return StatusCode(500, ex.Message);
			}			
		}		
	}
}
