using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  public record SqrtOperationResultModel
  {
    [JsonPropertyName("Square")]
    public int Result { get; set; }
  }
}
