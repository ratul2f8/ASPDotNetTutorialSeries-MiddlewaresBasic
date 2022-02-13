using Microsoft.AspNetCore.Mvc;

namespace middlewares_tut.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class HelloController : ControllerBase
	{
		[HttpGet]
		public async Task<string> GetHello()
		{
			return "Hello there...";
		}
	}
}
