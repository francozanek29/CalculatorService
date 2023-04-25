using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CalculatorService.Server.WebAPI.Validations;

namespace CalculatorService.Server.WebAPI.Models
{
     /// <summary>
    /// Record in charge of handling the information for a Add operation result.
    /// </summary>
    public record DivOperationModel
    {
        [Required]
        [JsonPropertyName("Dividend")]
        public int? Dividend { get; set; }

        [Required]
        [NotZero(ErrorMessage ="The Divisor should not be zero")]
        [JsonPropertyName("Divisor")]
        public int? Divisor { get; set; }
    }
}
