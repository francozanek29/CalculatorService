using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Journal Query operation result item in which the information
    /// is storaged.
    /// </summary>
    public record JournalModelResultItem
    {
        [JsonPropertyName("Operation")]
        public string Operation { get; set; }

        [JsonPropertyName("Calculation")]
        public string Calculation { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }
    }
}
