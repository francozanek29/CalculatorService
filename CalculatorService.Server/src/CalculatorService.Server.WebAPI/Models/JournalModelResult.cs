using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Journal Query operation result.
    /// </summary>
    public record JournalModelResult
    {
        [JsonPropertyName("Operations")]
        public IEnumerable<JournalModelResultItem> Operations { get; set; }
    }
}
