using Gtest.Common.Services.Interfaces;
using Gtest.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Gtest.Controllers;

[ApiController]
[Route("server/api/[controller]")]
public class GwpController(ILogger<GwpController> logger) : ControllerBase
{
    private readonly ILogger<GwpController> _logger = logger;
    private readonly ICountryGwpService _countryGwpService;

    [HttpPost("[action]")]
    public async Task<ActionResult<IEnumerable<LlinesOfBusiness>>> AvgAsync([FromBody] AvgRequest avgRequest)
    {
        try
        {
            _logger.LogInformation("Received request for average GWP for country: {Country} with LOBs: {LineOfBusiness}",
                avgRequest.Country, string.Join(", ", avgRequest.LineOfBusiness));

            var result = await _countryGwpService.AvgAsync(avgRequest);


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
