using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Multiply operation result.
    /// </summary>
    public record MultiplyOperationResultModel
    {
        [JsonPropertyName("Product")]
        public int Result { get; set; }
    }
}
