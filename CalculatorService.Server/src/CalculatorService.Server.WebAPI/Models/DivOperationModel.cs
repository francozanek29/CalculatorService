using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CalculatorService.Server.WebAPI.Validations;

namespace CalculatorService.Server.WebAPI.Models
{
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
