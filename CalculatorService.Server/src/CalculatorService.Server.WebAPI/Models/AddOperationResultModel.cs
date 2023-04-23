using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
  /// <summary>
  /// Record in charge of handling the information for a Add operation parameters in the controller. It receives the data from the 
  /// user and send the data to the application and viceversa. 
  /// </summary>
  public record AddOperationResultModel
  {
    [JsonPropertyName("Sum")]
    public int Sum { get; set; }  
  }
}
