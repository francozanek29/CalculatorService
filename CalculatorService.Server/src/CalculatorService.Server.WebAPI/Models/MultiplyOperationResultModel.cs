using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  public record MultiplyOperationResultModel
  {
    [JsonPropertyName("Product")]
    public int Result { get; set; }
  }
}
