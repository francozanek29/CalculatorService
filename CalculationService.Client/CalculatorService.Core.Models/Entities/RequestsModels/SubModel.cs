using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Core.Models.Entities.RequestsModels
{
    public record SubModel
    {
        [JsonPropertyName("Minuend")]
        public int? Minuend { get; set; }

        [JsonPropertyName("Subtrahend")]
        public int? Subtrahend { get; set; }
    }
}
