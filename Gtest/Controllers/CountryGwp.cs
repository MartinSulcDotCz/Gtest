using Gtest.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Gtest.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryGwp(ILogger<CountryGwp> logger) : ControllerBase
{
	private readonly ILogger<CountryGwp> _logger = logger;

	[HttpGet(Name = "avg")]
	public async Task<IEnumerable<LlinesOfBusiness>> GetAwgAsync(AvgRequest avgRequest)
	{
		return new List<LlinesOfBusiness>()
		{
			new LlinesOfBusiness
			{
				Transport = 1.0,
				liability = 2.0
			}
		};
	}
}
