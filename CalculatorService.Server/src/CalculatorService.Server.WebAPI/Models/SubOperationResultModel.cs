using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Sub operation result.
    /// </summary>
    public class SubOperationResultModel
    {
        [JsonPropertyName("Difference")]
        public int Result { get; set; }
    }
}
