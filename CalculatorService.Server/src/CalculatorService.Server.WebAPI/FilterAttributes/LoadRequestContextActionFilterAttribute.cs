using CalculatorService.Server.Core.Model.Entitites;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CalculatorService.Server.WebAPI.FilterAttributes
{
    public class LoadRequestContextActionFilterAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly RequestContext _requestContext;
        private const string TrackingIdHeaderName = "X-Evi-Tracking-Id";

        public LoadRequestContextActionFilterAttribute(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.ContainsKey(TrackingIdHeaderName))
            {
                _requestContext.TrackingId = context.HttpContext.Request.Headers[TrackingIdHeaderName];
            }

            await next.Invoke();
        }
    }
}
