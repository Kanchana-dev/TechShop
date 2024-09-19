using Shopping_App.Models;

namespace Shopping_App.Services
{
    public class ProductsService
	{
		private IAppHttpService _appHttpService;
        private ILogger<ProductsService> _logger;

        public ProductsService(AppHttpService appHttpService, ILogger<ProductsService> logger)
		{			
			_appHttpService = appHttpService;
			_logger = logger;
		}
		public async Task<List<Product>> GetAllProducts()
		{
			try
			{
				_logger.LogInformation("In Products Service getting all Products");
				var result = await _appHttpService.Get<List<Product>>($"api/products", "products");
                _logger.LogInformation("Successful retrieved all Products");

                return result!;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex.InnerException,ex.StackTrace);
				return null;
			}
		}

		public async Task<List<int>> GetQuantity()
		{
			try
			{
				_logger.LogInformation("In Products Service getting Quantity");
				var result = await _appHttpService.Get<List<int>>($"api/products/quantity", "products");
                _logger.LogInformation("Successful retrieved Quantity");

                return result!;
			}
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, ex.StackTrace);
                return null;
            }
        }
	}
	
}
