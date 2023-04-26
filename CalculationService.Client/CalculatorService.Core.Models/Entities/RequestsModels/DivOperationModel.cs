using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Core.Models.Entities.RequestsModels
{
    public record DivOperationModel
    {
        [JsonPropertyName("Dividend")]
        public int? Dividend { get; set; }

        [Required]
        [JsonPropertyName("Divisor")]
        public int? Divisor { get; set; }
    }
}
