using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Div operation result.
    /// </summary>
    public record DivOperationResultModel
    {
        [JsonPropertyName("Quotient")]
        public int Result { get; set; }

        [JsonPropertyName("Remainder")]
        public int Remainder { get; set; }
    }
}
