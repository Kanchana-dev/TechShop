using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OrdersAPI.Middleware
{
	public class OrdersAPIGlobalExceptionHandler: IExceptionHandler
	{
		private readonly ILogger<OrdersAPIGlobalExceptionHandler> _logger;

		public OrdersAPIGlobalExceptionHandler(ILogger<OrdersAPIGlobalExceptionHandler> logger)
		{
			_logger = logger;
		}

		async ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			_logger.LogError(exception, $"Exception occurred: {exception.Message}", exception.StackTrace);

			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Server error",
				Detail = exception.Source
			};

			httpContext.Response.StatusCode = problemDetails.Status.Value;
			await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

			return true;
		}
	}
}
