using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Sub operation parameters in the controller. 
    /// It receives the data from the user and send the data to the application and viceversa. 
    /// </summary>
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
