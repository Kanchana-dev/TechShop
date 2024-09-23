
using OrdersAPI.Models;

namespace OrdersAPI.Repositories
{
	public interface IOrdersRepository
	{
		Task<List<Order>> GetAllOrders(CancellationToken ct=default);		
		Task<bool> AddOrder(Order order,CancellationToken ct= default);
	}
}
