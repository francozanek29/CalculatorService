using CalculatorService.Server.WebAPI.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  /// <summary>
  /// Record in charge of handling the information for a Add operation parameters in the controller. 
  /// It receives the data from the user and send the data to the application and viceversa. 
  /// </summary>
  public record AddOperationModel
  {
    [Required]
    [JsonPropertyName("Addends")]
    [EnsureMinimumElementsAttribute(2, ErrorMessage = "At least two elements should be provided")]
    public IEnumerable<int> Addends { get; set; } = new List<int>();
  }
}
