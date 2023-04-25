using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI.Models
{
    public record JornalModel
    {
        [Required]
        [JsonPropertyName("Id")]
        public string TrackingId { get; set; }
    }
}
