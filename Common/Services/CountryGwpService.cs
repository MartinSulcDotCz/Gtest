using Gtest.Common.DataForTest;
using Gtest.Common.Services.Interfaces;
using Gtest.Models.ApiModels;
using Gtest.Models.DbModels;
using Microsoft.Data.Sqlite;

namespace Gtest.Common.Services;

public class CountryGwpService(VirtualDatabase virtualDatabase) : ICountryGwpService
{
	private readonly VirtualDatabase _virtualDatabase = virtualDatabase ?? throw new ArgumentNullException(nameof(virtualDatabase));

	public async Task<AvgBylineOfBusiness> AvgAsync(AvgRequestDto avgRequest)
	{
		AvgBylineOfBusiness avgBylineOfBusiness = new();
		SqliteConnection connection = _virtualDatabase.Connection;

		foreach (string lineOfBusiness in avgRequest.LineOfBusiness)
		{
			using var cmd = connection.CreateCommand();
			cmd.CommandText = @"
							SELECT
							(				
								CAST(Y2008 AS REAL) +
								CAST(Y2009 AS REAL) +
								CAST(Y2010 AS REAL) +
								CAST(Y2011 AS REAL) +
								CAST(Y2012 AS REAL) +
								CAST(Y2013 AS REAL) +
								CAST(Y2014 AS REAL) +
								CAST(Y2015 AS REAL)
							) / 8.0 AS Average
							FROM [gwpByCountry] 
							WHERE [country] = @country AND [lineOfBusiness] = @lineOfBusiness";
			cmd.Parameters.AddWithValue("@country", avgRequest.Country ?? string.Empty);
			cmd.Parameters.AddWithValue("@lineOfBusiness", lineOfBusiness);
			object? avg = await cmd.ExecuteScalarAsync();
			double avgLineOfBusiness = (avg is DBNull || avg == null) ? 0.0 : Convert.ToDouble(avg);
			avgBylineOfBusiness.AvgByLineOfBusiness.Add(lineOfBusiness, avgLineOfBusiness);
		}

		return avgBylineOfBusiness;
	}
}