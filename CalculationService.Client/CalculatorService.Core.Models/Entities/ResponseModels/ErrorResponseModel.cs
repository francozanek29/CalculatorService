using System.Text.Json.Serialization;

namespace CalculatorService.Core.Models.Entities.ResponseModels
{
    public record ErrorResponseModel
    {
        [JsonPropertyName("ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("ErrorStatus")]
        public int ErrorStatus { get; set; }

        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}
