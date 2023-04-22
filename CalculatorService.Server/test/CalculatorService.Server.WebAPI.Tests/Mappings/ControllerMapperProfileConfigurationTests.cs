using AutoMapper;
using CalculatorService.Server.WebAPI.Mappings;

namespace CalculatorService.Server.WebAPI.Tests.Mappings
{
  public class ControllerMapperProfileConfigurationTests
  {
    [Fact]
    public void AllDestinationFieldsAreConfigured()
    {

      /*REMARKS Automatic testing of configuration completeness: All non-excluded destionation fields are 
      have mapping configuration, implicit or explicit.*/

      new MapperConfiguration(cfg => cfg.AddProfile(new ControllerMapperProfile()))
          .AssertConfigurationIsValid();
    }
  }
}
