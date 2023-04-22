using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.WebAPI.Validations
{
  /// <summary>
  /// Custom validation to validate collections to have at least 2 elements as it´s described in the requirements.
  /// </summary>
  public class EnsureMinimumElementsAttribute : ValidationAttribute
  {
    private readonly int _minElements;
    public EnsureMinimumElementsAttribute(int minElements)
    {
      _minElements = minElements;
    }

    public override bool IsValid(object value)
    {
      var list = value as IList;

      if (list != null)
      {
        return list.Count >= _minElements;
      }
      return false;
    }
  }
}
