using Shopping_App.Models;

namespace Shopping_App.Services
{
    public class OrdersService : IOrdersService
    {
        private IAppHttpService _appHttpService;
        private ILogger<OrdersService> _logger;
        public OrdersService(AppHttpService appHttpService, ILogger<OrdersService> logger)
        {
            _appHttpService = appHttpService;
            _logger = logger;
        }

        public async Task<List<Order>> GetAllOrdersByUser(Guid userId)
        {
            try
            {
                _logger.LogInformation($"In Orders Service getting Orders by UserId - {userId}");
                var result = await _appHttpService.Get<List<Order>>($"api/orders/getallorders/{userId}", "orders");
                _logger.LogInformation($"Successful retrieved Orders by UserId - {userId}");

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> AddOrder(Order order)
        {
            try
            {
                _logger.LogInformation($"In Orders Service Adding Order - {order}");

                var result = await _appHttpService.Post<bool>($"api/orders/addorder", order, "orders");               

                _logger.LogInformation($"Successful added Order - {order}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, ex.StackTrace);
                return false;
            }
        }

    }


}
