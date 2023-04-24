using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.WebAPI.Validations
{
  public class NotZeroAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      if(value != null)
      {
        return (int)value != 0;
      }

      return true;
    }
  }
}
