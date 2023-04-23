using AutoMapper;
using CalculatorService.Server.Repository.Mapping;

namespace CalculatorService.Server.Repository.Tests.Mappings
{
    public class RepositoryMapperProfileConfigurationTest
  {
    [Fact]
    public void AllDestinationFieldsAreConfigured()
    {

      /*REMARKS Automatic testing of configuration completeness: All non-excluded destionation fields are 
      have mapping configuration, implicit or explicit.*/

      new MapperConfiguration(cfg => cfg.AddProfile(new RepositoryMapperProfile()))
          .AssertConfigurationIsValid();
    }
  }
}