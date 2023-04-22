using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  /// <summary>
  /// Record designed to handle the errors and show them as expected.
  /// </summary>
  public record ErrorDescriptionClass
  {
    [JsonPropertyName("ErrorCode")]
    public string ErrorCode { get; set; }

    [JsonPropertyName("ErrorStatus")]
    public int ErrorStatus { get; set; }

    [JsonPropertyName("ErrorMessage")]
    public string ErrorMessage { get; set; }  
  }
}
