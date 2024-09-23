using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrdersAPI.BusinessRules;
using OrdersAPI.Models;

namespace OrdersAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
				return Ok(await _ordersBusinessRules.GetAllOrders(ct));
			}
			catch (OperationCanceledException cte)
			{
				_logger.LogError($"Client cancelled Task: {cte.Message}{Environment.NewLine}{cte.StackTrace} ");
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return StatusCode(500, ex.Message);
			}			
		}


		// GET api/<OrdersController>/5
		[HttpGet("getallorders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id, CancellationToken ct=default)
		{
            _logger.LogInformation($"Get Orders by UserId - {id}");
            if (id == Guid.Empty)
            {
                _logger.LogInformation("UserId is empty");
                return BadRequest();
            }
            try
            {		
				return Ok(await _ordersBusinessRules.GetUserOrders(id, ct));
            }
            catch (Exception ex)
            {
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return StatusCode(500, ex.Message);
            }
        }

		// POST api/<OrdersController>
		[HttpPost]
		[Route("addorder")]		
		[ProducesResponseType<Order>(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
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
				return Ok(await _ordersBusinessRules.AddOrder(order, ct));		  
			}
			catch (TaskCanceledException cte)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return StatusCode(500, ex.Message);
			}			
		}		
	}
}
