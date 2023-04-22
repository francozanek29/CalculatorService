using CalculatorService.Server.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CalculatorService.Server.WebAPI.FilterAttributes
{
  /// <summary>
  /// This class can handle exceptions that happen in the application,in a generic manner like redirecting to an 
  /// error page or sending back a generic exception message the json response. The exception filter attribute 
  /// itself is not an exception, it handles the exception.
  /// </summary>
  public class HandledExceptionFilterAttribute : ExceptionFilterAttribute
  {
    private readonly string ErrorMessage = "There was an error during the request try again.";

    public override void OnException(ExceptionContext context)
    {
      var response = new ErrorDescriptionClass()
      {
        ErrorCode = "InternalError",
        ErrorStatus = (int)HttpStatusCode.InternalServerError,
        ErrorMessage = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support"
      };

      context.Result = new ObjectResult(response)
      {
        StatusCode = (int)HttpStatusCode.InternalServerError
      };
    }

  }
}
