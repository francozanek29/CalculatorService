using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    public record JornalModelResultItem
    {
        [JsonPropertyName("Operation")]
        public string Operation { get; set; }

        [JsonPropertyName("Calculation")]
        public string Calculation { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }
    }
}
