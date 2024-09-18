using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Middlerware
{
	public class APIGlobalException : IExceptionHandler
	{
		private readonly ILogger<APIGlobalException> _logger;

		public APIGlobalException(ILogger<APIGlobalException> logger)
		{
			_logger = logger;
		}

		async ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			_logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

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
