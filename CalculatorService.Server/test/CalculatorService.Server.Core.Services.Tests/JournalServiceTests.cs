namespace CalculatorService.Server.Core.Services.Tests
{
  public class JournalServiceTests
  {
    private JournalService _sut;
    private readonly Mock<IRepository> _mockRepository = new();
    private readonly Mock<ILogger> _mockLogger = new();

    public JournalServiceTests()
    {
      _sut = new(_mockRepository.Object, _mockLogger.Object);
    }

    /// <summary>
    /// Test case: When there is some error catched in the Service class, log the error and throw the exception.
    /// </summary>
    [Fact]
    public async Task WhenThereWasAnErrorInTheRepository_ThrowExceptionAndLogTheException()
    {
      //Arrange
      _mockRepository.Setup(mr => mr.GetAllOperationInfosByTrackingIdAsync(It.IsAny<string>()))
        .ThrowsAsync(new Exception());

      Func<Task> action = () => _sut.GetJornalOperationByIdAsync("someTrackingId");

      //Act - Assert
      using (new AssertionScope())
      {
        await action.Should().ThrowAsync<Exception>();
        _mockLogger.Verify(ml => ml.Error(It.IsAny<Exception>(), "There was an error during the execution of the request to get the information from the Database"));
      }
    }
  }
}
