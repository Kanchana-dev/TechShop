using Shopping_App.Models;

namespace Shopping_App.Services
{
    public interface IOrdersService
    {
        Task<List<Order>> GetAllOrdersByUser(Guid userId);
        Task<bool> AddOrder(Order order);

	}
}
