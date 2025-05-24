using Gtest.Common.Services.Interfaces;
using Gtest.Models.ApiModels;

namespace Gtest.Common.Services;

public class CountryGwpService : ICountryGwpService
{
    public Task<IEnumerable<object>> AvgAsync(AvgRequest avgRequest)
    {
        throw new NotImplementedException();
    }
}