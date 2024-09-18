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
			var orders = _ordersRepo.GetAllOrders(ct).Result;
			if (orders != null && orders.Count > 0)
			{
				return orders;
			}

			return new List<Order>();
		}

        public async Task<List<Order>> GetUserOrders(int userId, CancellationToken ct=default)
        {
            var orders = _ordersRepo.GetAllOrders(ct).Result.ToList();
            if (orders != null && orders.Count > 0)
            {
               return orders.Where(x => x.UserId == userId).ToList();				
            }

            return new List<Order>();
        }
       
		public async Task<bool> AddOrder(Order order)
		{
			var result = _ordersRepo.AddOrder(order);
			return true;
		}
	}
}
