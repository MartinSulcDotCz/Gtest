using Gtest.Common.Core.Enums;
using Gtest.Common.Helpers;
using Gtest.Common.Services.Interfaces;
using Gtest.Models.ApiModels;
using Gtest.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Gtest.Controllers;

[ApiController]
[Route("server/api/[controller]")]
public class GwpController(ICountryGwpService countryGwpService, ILogger<GwpController> logger) : ControllerBase
{
	private readonly ILogger<GwpController> _logger = logger;
	private readonly ICountryGwpService _countryGwpService = countryGwpService ?? throw new ArgumentNullException(nameof(countryGwpService));

	/// <summary>
	/// It returns an average gross written premium (GWP) over 2008-2015 period for the selected lines of business and country.
	/// </summary>
	/// <param name="avgRequest">
	/// Country is ISO 639-1 code, e.g. "en" for English, "fr" for French.
	/// LineOfBusiness is a list of strings representing the lines of business for which the average GWP is requested. <see cref="Gtest.Common.Core.Enums.LineOfBusinessEnum"/>
	/// </param>
	/// <returns></returns>
	[HttpPost("[action]")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<Dictionary<LineOfBusinessEnum, double>>> AvgAsync([FromBody] AvgRequestDto avgRequest)
	{
		try
		{
			_logger.LogInformation("Received request for average GWP for country: {Country} with LOBs: {LineOfBusiness}",
				avgRequest.Country, string.Join(", ", avgRequest.LineOfBusiness));

			//if (avgRequest.Country == null || !avgRequest.Country.IsValidLanguageCode())
			//{
			//	return BadRequest("Invalid country code. Please provide a valid ISO 639-1 language code.");
			//}

			if (avgRequest.LineOfBusiness != null && !avgRequest.LineOfBusiness.IsValidLineOfBusiness())
			{
				return BadRequest("Invalid LineOfBusiness code. Please provide a valid LineOfBusiness code.");
			}

			//if (avgRequest.IsInCache()) // Cache the results of each request and return the cached result if available
			//{
			//	return FromCache;
			//}

			AvgBylineOfBusiness result = await _countryGwpService.AvgAsync(avgRequest);

			// Cache the results of each request and return the cached result if available. Redis?

			return result.AvgByLineOfBusiness;
		}
		catch (Exception)
		{
			return Problem("An error occurred while processing the request.", statusCode: StatusCodes.Status500InternalServerError);
		}
	}
}