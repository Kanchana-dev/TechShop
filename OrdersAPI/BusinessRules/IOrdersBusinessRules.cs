using OrdersAPI.Models;

namespace OrdersAPI.BusinessRules
{
	public interface IOrdersBusinessRules
	{
		Task<List<Order>> GetAllOrders(CancellationToken ct = default);
		Task<List<Order>> GetUserOrders(int userId, CancellationToken ct=default);        
		Task<bool> AddOrder(Order order);
	}
}
