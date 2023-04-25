using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Add operation result.
    /// </summary>
    public record AddOperationResultModel
    {
        [JsonPropertyName("Sum")]
        public int Result { get; set; }
    }
}
