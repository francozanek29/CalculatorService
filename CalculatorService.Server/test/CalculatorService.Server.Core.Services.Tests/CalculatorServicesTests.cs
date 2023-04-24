namespace CalculatorService.Server.Core.Services.Tests
{
  public partial class CalculatorServicesTests
  {
    private CalculatorServices _sut;
    private readonly Mock<IRepository> _mockRepository = new();
    private readonly RequestContext _requestContextForTracking;
    private readonly RequestContext _requestContextForNoTracking;

    public CalculatorServicesTests()
    {
      _requestContextForTracking = new()
      {
        TrackingId = "someTrackingId"
      };

      _requestContextForNoTracking = new();
    }
  }
}
