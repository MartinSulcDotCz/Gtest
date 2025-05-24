using System.Text.Json.Serialization;

namespace Gtest.Models.ApiModels;

public class AvgRequestDto
{

    public string? Country { get; set; }


    [JsonPropertyName("lob")]
    public IEnumerable<string> LineOfBusiness { get; set; }
}