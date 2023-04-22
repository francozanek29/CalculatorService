using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorService.Server.Bootstrapper
{
  public static class ApplicationServicesBootstrapper
  {
    /// <summary>
    /// Definition for all the dependecies in the code. The idea is to keep in centralized in one place
    /// so all the configuration defined by the user is centralized here.
    /// </summary>
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
      ConfigureDomainServices(services);
      ConfigureIntegrationServices(services);
    }

    /// <summary>
    /// Centralized all the dependencies definition for the Service project
    /// </summary>
    /// <remarks>Centralized all the dependencies definition for the Service project</remarks>
    /// <response void>OK. When the service is ready to receive requests.</response>
    /// 

    private static void ConfigureDomainServices(IServiceCollection services)
    {
      services.AddScoped<ICalculatorService, CalculatorServices>();
    }

    /// <summary>
    /// Centralized all the dependencies definition for the Integration project
    /// </summary>
    /// <remarks>Centralized all the dependencies definition for the Integration project</remarks>
    /// <response void>OK. When the service is ready to receive requests.</response>
    /// 

    private static void ConfigureIntegrationServices(IServiceCollection services)
    {
      
    }
  }
}