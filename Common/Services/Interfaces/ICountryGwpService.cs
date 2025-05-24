using Gtest.Models.ApiModels;
using Gtest.Models.DbModels;

namespace Gtest.Common.Services.Interfaces;

public interface ICountryGwpService
{
    Task<AvgBylineOfBusiness> AvgAsync(AvgRequestDto avgRequest);
}