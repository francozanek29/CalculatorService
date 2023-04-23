using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  public record SubOperationModel
  {
    [Required]
    [JsonPropertyName("Minuend")]
    public int? Minuend { get; set; }

    [Required]
    [JsonPropertyName("Subtrahend")]
    public int? Subtrahend { get; set; }
  }
}
