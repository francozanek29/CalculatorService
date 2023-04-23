namespace CalculatorService.Server.WebAPI.Tests.ControllerTests
{
  public partial class CalculatorControllerTest : IClassFixture<TestApplication>
  {
    private readonly TestApplication _testApplication;

    private (HttpClient HttpClient, TestApplicationDataSourceMocks DataSourceMocks) _testClient;

    private const string TrackingHeader = "X-Evi-Tracking-Id";

    public CalculatorControllerTest(TestApplication testApplication)
    {
      _testApplication = testApplication;

      _testClient = _testApplication.SetupTestRequest();
    }

  }
}
