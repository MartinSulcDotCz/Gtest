using Gtest.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Gtest.Controllers;

[ApiController]
[Route("server/api/gwp")]
public class CountryGwp(ILogger<CountryGwp> logger) : ControllerBase
{
    private readonly ILogger<CountryGwp> _logger = logger;

    [HttpPost(Name = "avg")]
    public async Task<ActionResult<IEnumerable<LlinesOfBusiness>>> GetAwgAsync([FromBody] AvgRequest avgRequest)
    {
        try
        {
            _logger.LogInformation("Received request for average GWP for country: {Country} with LOBs: {LineOfBusiness}",
                avgRequest.Country, string.Join(", ", avgRequest.LineOfBusiness));

            return new List<LlinesOfBusiness>()
            {
                new LlinesOfBusiness
                {
                    Transport = 1.0,
                    liability = 2.0
                }
            };

        }
        catch (Exception)
        {
            return Problem("An error occurred while processing the request.");
        }
    }
}
