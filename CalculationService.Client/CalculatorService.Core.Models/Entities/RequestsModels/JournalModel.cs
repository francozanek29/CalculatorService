using System.Text.Json.Serialization;

namespace CalculatorService.Core.Models.Entities.RequestsModels
{
    public record JournalModel
    {
        [JsonPropertyName("Id")]
        public string TrackingId { get; set; }
    }
}
