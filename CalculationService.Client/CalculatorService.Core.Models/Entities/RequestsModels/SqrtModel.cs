using System.Text.Json.Serialization;

namespace CalculatorService.Core.Models.Entities.RequestsModels
{
    public record SqrtModel
    {
        [JsonPropertyName("Number")]
        public int Number { get; set; }
    }
}
