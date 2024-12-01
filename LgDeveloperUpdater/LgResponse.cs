using System.Text.Json.Serialization;

namespace LgDeveloperUpdater;
internal record LgResponse
{
    [JsonPropertyName("result")]
    public required string Result { get; set; }
    [JsonPropertyName("errorCode")]
    public required string ErrorCode { get; set; }
    [JsonPropertyName("errorMsg")]
    public required string ErrorMsg { get; set; }
}