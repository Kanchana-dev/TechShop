using Shopping_App.Models;

namespace Shopping_App.Services
{
    public interface IOrdersService
    {
        Task<List<Order>> GetAllOrdersByUser(int userId);
        Task<bool> AddOrder(Order order);

	}
}
