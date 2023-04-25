using CalculatorService.Server.WebAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.WebAPI.Models
{
    /// <summary>
    /// Record in charge of handling the information for a Multiply operation parameters in the controller. 
    /// It receives the data from the user and send the data to the application and viceversa. 
    /// </summary>
    public record MultiplyOperationModel
    {
        [Required]
        [EnsureMinimumElementsAttribute(2, ErrorMessage = "At least two elements should be provided")]
        public IEnumerable<int> Factors { get; set; } = new List<int>();
    }
}
