using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.WebAPI.Validations
{
    public class NotZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) => (int)value != 0;
    }
}
