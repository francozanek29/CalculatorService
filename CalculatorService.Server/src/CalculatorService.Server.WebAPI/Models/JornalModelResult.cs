using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    public record JornalModelResult
    {
        [JsonPropertyName("Operations")]
        public IEnumerable<JornalModelResultItem> Operations { get; set; }
    }
}
