using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Sqrt operation parameters in the controller. 
    /// It receives the data from the user and send the data to the application and viceversa. 
    /// </summary>
    public record SqrtOperationModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The number should be greater thatn zero")]
        [JsonPropertyName("Number")]
        public int Number { get; set; }
    }
}
