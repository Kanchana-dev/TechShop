using OrdersAPI.Models;
using OrdersAPI.Repositories;

namespace OrdersAPI.BusinessRules
{
	public class OrdersBusinessRules : IOrdersBusinessRules
	{
		private readonly IOrdersRepository _ordersRepo;
		private readonly ILogger<OrdersBusinessRules> _logger;
		
		public OrdersBusinessRules(IOrdersRepository ordersRepo, ILogger<OrdersBusinessRules> logger)
		{
			_ordersRepo = ordersRepo;
			_logger = logger;
		}

		public async Task<List<Order>> GetAllOrders(CancellationToken ct = default)
		{
			var orders = await _ordersRepo.GetAllOrders(ct);
			if (orders != null && orders.Count > 0)
			{
				return orders;
			}

			return new List<Order>();
		}

        public async Task<List<Order>> GetUserOrders(Guid userId, CancellationToken ct=default)
        {
            var orders = await _ordersRepo.GetAllOrders(ct);
            if (orders != null && orders.Count > 0)
            {
               return orders.Where(x => x.UserId == userId).ToList();				
            }

            return new List<Order>();
        }
       
		public async Task<bool> AddOrder(Order order, CancellationToken ct = default)
		{
			var result = await _ordersRepo.AddOrder(order,ct);
			return result;		
		}
	}
}
