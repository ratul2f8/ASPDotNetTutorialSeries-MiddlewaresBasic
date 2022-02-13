using System.Net;

namespace middlewares_tut.Middlewares
{
	public class HelloMiddleware : IMiddleware
	{
		private readonly ILogger<TestingMiddleware>? _logger;
		public HelloMiddleware(ILoggerFactory logger)
		{

			_logger = logger.CreateLogger<TestingMiddleware>();
		}
		public async Task HanldeHelloContext(HttpContext context, RequestDelegate next)
		{
			try
			{
				_logger?.LogInformation("Before hello request");
				await next(context);
				_logger?.LogInformation("After hello request");
			}
			catch (Exception e)
			{
				_logger?.LogError(e.Message);
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.Response.WriteAsJsonAsync(e.Message);
			}
		}

		public Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			throw new NotImplementedException();
		}
	}
}
