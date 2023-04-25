using CalculatorService.Server.Core.Model.Interfaces;
using Moq;

namespace CalculatorService.Server.WebAPI.Tests
{
    public class TestApplicationDataSourceMocks
    {
        public Mock<IRepository> MockRepository = new();
    }
}
