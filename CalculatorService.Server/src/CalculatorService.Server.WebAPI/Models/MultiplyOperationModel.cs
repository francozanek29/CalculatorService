using CalculatorService.Server.WebAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.WebAPI.Models
{
  public record MultiplyOperationModel
  {
    [Required]
    [EnsureMinimumElementsAttribute(2, ErrorMessage = "At least two elements should be provided")]
    public IEnumerable<int> Factors { get; set; } = new int[0];
  }
}
