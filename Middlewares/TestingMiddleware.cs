using System.Net;

namespace middlewares_tut.Middlewares
{
	public class TestingMiddleware : IMiddleware
	{
		private readonly ILogger<TestingMiddleware>? _logger;
		public TestingMiddleware(ILoggerFactory logger)
		{

			_logger = logger.CreateLogger<TestingMiddleware>();
		}
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				_logger?.LogInformation("Before request");
				await next(context);
				_logger?.LogInformation("After request");
			}
			catch (Exception e)
			{
				_logger?.LogError(e.Message);
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.Response.WriteAsJsonAsync(e.Message);
			}
		}
	}
}
