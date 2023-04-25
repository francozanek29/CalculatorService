using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CalculatorService.Server.WebAPI.Tests
{
    public class TestApplication
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory = new();

        private static void WireExternalDataMocks(IServiceCollection services, TestApplicationDataSourceMocks testingMocks)
        {
            RegisterMock(testingMocks.MockRepository.Object);

            void RegisterMock<T>(T mock) where T : class
            {
                services.RemoveAll<T>();
                services.AddScoped<T>(_ => mock);
            }
        }

        public (HttpClient HttpClient, TestApplicationDataSourceMocks DataSourceMocks) SetupTestRequest(Action<IServiceCollection>? configureTestServicesAction = null)
        {
            var testingMocks = new TestApplicationDataSourceMocks();

            HttpClient httpClient = _webApplicationFactory.WithWebHostBuilder(builder => builder.ConfigureTestServices(SetupMocksAndTestServices))
                                     .CreateDefaultClient();

            return (httpClient, testingMocks);

            void SetupMocksAndTestServices(IServiceCollection services)
            {
                WireExternalDataMocks(services, testingMocks);

                if (configureTestServicesAction != null)
                {
                    configureTestServicesAction.Invoke(services);
                }
            }
        }
    }
}
