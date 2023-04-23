namespace CalculatorService.Server.Core.Services.Tests
{
  public partial class CalculatorServicesTests
  {
    private readonly CalculatorServices _sut;
    private readonly Mock<IRepository> _mockRepository = new();

    public CalculatorServicesTests()
    {
      _sut = new(_mockRepository.Object);
    }
  }
}
