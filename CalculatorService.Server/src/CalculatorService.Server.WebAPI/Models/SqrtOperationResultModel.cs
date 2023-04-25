using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Sqrt operation result.
    /// </summary>
    public record SqrtOperationResultModel
    {
        [JsonPropertyName("Square")]
        public int Result { get; set; }
    }
}
