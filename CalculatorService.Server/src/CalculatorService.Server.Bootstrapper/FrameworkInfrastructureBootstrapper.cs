using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CalculatorService.Server.Bootstrapper
{
  public static class FrameworkInfrastructureBootstrapper
  {
    /// <summary>
    /// Defintion for all the configuration needed for the application. This configuration is defined by the user
    /// while the application is designed.
    /// </summary>
    /// <response void>response>
    /// 
    public static void ConfigureFrameworkInfrastructure(this IServiceCollection services, Assembly webAPIAssembly)
    {
      //Remarks: No need to explicitly add all Profiles, params are either Assemblies or Types to scan their assembly       
      services.AddAutoMapper(webAPIAssembly);

      //services.AddAutoMapper(typeof(IntegrationMapperProfile));

      //services.AddDbContext<CarPoolingRepositoryDbContext>(options => options.UseInMemoryDatabase("CarPoolingAPI"));
    }
  }
}
