using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CalculatorService.Server.WebAPI.FilterAttributes
{
  public class ValidationFilterAttribute : IActionFilter
  {
    /// <summary>
    /// Action Filter that handles DomainValidationException to return consistant error messages.
    /// Error message format is shared with automatic ModelBinding validation
    /// </summary>

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      ModelStateDictionary modelState = context.ModelState;

      if (!modelState.IsValid)
      {
        var errorDictionary = modelState
                              .Where(k => k.Value!.Errors.Count > 0)
                              .ToDictionary(k => k.Key,
                                    p => string.Join(" - ", p.Value!.Errors.Select(error => error.ErrorMessage)));


        var response = new
        {
          Message = "Invalid request",
          Errors = errorDictionary
        };

        context.Result = new ObjectResult(response)
        {
          StatusCode = (int)HttpStatusCode.BadRequest
        };
      }
    }
  }
}
