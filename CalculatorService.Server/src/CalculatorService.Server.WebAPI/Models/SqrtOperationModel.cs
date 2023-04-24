using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  public record SqrtOperationModel
  {
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The number should be greater thatn zero")]
    [JsonPropertyName("Number")]
    public int Number { get; set; }
  }
}
