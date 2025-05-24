using Gtest.Models.ApiModels;

namespace Gtest.Common.Services.Interfaces;

public interface ICountryGwpService
{
    Task<IEnumerable<object>> AvgAsync(AvgRequest avgRequest);
}