using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  public class SubOperationResultModel
  {
    [JsonPropertyName("Difference")]
    public int Result { get; set; }
  }
}
