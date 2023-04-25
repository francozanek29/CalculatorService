using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.WebAPI.Validations
{
    /// <summary>
    /// Custom validation to validate number not to be zero.
    /// </summary>
    public class NotZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                return (int)value != 0;
            }

            return true;
        }
    }
}
