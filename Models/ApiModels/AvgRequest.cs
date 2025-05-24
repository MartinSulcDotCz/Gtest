using System.Text.Json.Serialization;

namespace Gtest.Models.ApiModels;

public class AvgRequest
{

    public string? Country { get; set; }


    [JsonPropertyName("lob")]
    public IEnumerable<string> LineOfBusiness { get; set; }
}