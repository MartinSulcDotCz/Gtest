using Gtest.Common.Core.Enums;
using Gtest.Common.DataForTest;
using Gtest.Common.Services;
using Gtest.Common.Services.Interfaces;
using Gtest.Models.ApiModels;
using Gtest.Models.DbModels;

namespace Gtest.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task GetAvgAsync()
		{
			// Arrange
			using VirtualDatabase virtualDatabase = new();
			ICountryGwpService countryGwpService = new CountryGwpService(virtualDatabase);
			AvgRequestDto avgRequest = new()
			{
				Country = "au",
				LineOfBusiness = [nameof(LineOfBusinessEnum.freight), nameof(LineOfBusinessEnum.transport)]
			};

			// Act
			AvgBylineOfBusiness result = await countryGwpService.AvgAsync(avgRequest);

			// Assert
			Assert.That(result.AvgByLineOfBusiness, Has.Count.EqualTo(2)); // Assuming we requested two lines of business
			Assert.That(result.AvgByLineOfBusiness, Contains.Key(LineOfBusinessEnum.freight));
			Assert.That(result.AvgByLineOfBusiness, Contains.Value(406664133.35));
			Assert.That(result.AvgByLineOfBusiness, Contains.Key(LineOfBusinessEnum.transport));
			Assert.That(result.AvgByLineOfBusiness, Contains.Value(220083912.625));
		}
	}
}
