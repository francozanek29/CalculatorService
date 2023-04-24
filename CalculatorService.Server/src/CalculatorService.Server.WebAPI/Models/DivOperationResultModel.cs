using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    public record DivOperationResultModel
    {
        [JsonPropertyName("Quotient")]
        public int Result { get; set; } 
        
        [JsonPropertyName("Remainder")]
        public int Remainder { get; set; }
    }
}
