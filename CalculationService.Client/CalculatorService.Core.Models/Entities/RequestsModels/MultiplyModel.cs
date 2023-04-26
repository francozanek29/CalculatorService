using System.Text.Json.Serialization;

namespace CalculatorService.Core.Models.Entities.RequestsModels
{
    public record MultiplyModel
    {
        [JsonPropertyName("Factors")]
        public IEnumerable<int> Operands { get; set; } = new List<int>();
    }
}
