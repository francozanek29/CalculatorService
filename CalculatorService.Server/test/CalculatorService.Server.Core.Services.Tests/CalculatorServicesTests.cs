using Serilog;

namespace CalculatorService.Server.Core.Services.Tests
{
  /// <summary>
  /// This class is the principal one, here is the definition for all the properties that should be used for all the other classes
  /// and also defined some test that are general to all the posible scenarios and it does not make much more sense to write on
  /// each file.
  /// </summary>
  public partial class CalculatorServicesTests
  {
    private CalculatorServices _sut;
    private readonly Mock<IRepository> _mockRepository = new();
    private readonly Mock<ILogger> _mockLogger = new();
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

    /// <summary>
    /// Test case: When there is some error catched in the Service class, log the error and throw the exception.
    /// </summary>
    [Fact]
    public async Task WhenThereWasAnErrorInTheRepository_ThrowExceptionAndLog()
    {
      //Arrange
      _mockRepository.Setup(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()))
        .ThrowsAsync(new Exception());

      _sut = new CalculatorServices(_mockRepository.Object, _requestContextForTracking, _mockLogger.Object);

      Func<Task> action = () => _sut.ExecuteOperation(new OperationDTOOperands() {Operands = new List<int>() { 2,3} }, '+');

      //Act - Assert
      using (new AssertionScope())
      {
        await action.Should().ThrowAsync<Exception>();
        _mockLogger.Verify(ml => ml.Error(It.IsAny<Exception>(), "There was an error during the execution of the operation requested"));
      }
    }
  }
}
