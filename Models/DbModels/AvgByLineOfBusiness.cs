using Gtest.Common.Core.Enums;

namespace Gtest.Models.DbModels;

public class AvgBylineOfBusiness
{
	public Dictionary<LineOfBusinessEnum, double> AvgByLineOfBusiness { get; set; } = [];
}