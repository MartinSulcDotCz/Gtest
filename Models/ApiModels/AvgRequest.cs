using Gtest.Common.Core.Enums;

namespace Gtest.Models.ApiModels;

public class AvgRequest
{

	public string? Country { get; set; }


	[System.Text.Json.Serialization.JsonPropertyName("lob")]
	public IEnumerable<LineOfBusinessEnum> LineOfBusiness { get; set; }
}