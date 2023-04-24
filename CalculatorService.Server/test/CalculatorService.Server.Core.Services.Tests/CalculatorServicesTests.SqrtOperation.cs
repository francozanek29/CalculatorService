namespace CalculatorService.Server.Core.Services.Tests
{
  public partial class CalculatorServicesTests
  {
    /// <summary>
    /// Test cases: When the elements to be added are sent, the result is correct, and not tracking Id is sent,
    /// the repository is not being called.
    /// </summary>
    /// <param name="elementsToBeAdded"></param>
    /// <param name="expectedResult"></param>
    /// <returns></returns>
    [Theory]
    [MemberData(nameof(DataAdd))]
    public async Task SqrtElementsAsync_WhenElementsAreSentAndNotTrackingIdISent_ReturnsSum(List<int> elementsToBeAdded, int expectedResult)
    {
      //Arrange
      var addOperationDto = new OperationDTOOperands()
      {
        Operands = elementsToBeAdded
      };

      //Act 
      var addOperationResult = await _sut.AddElementsAsync(addOperationDto, string.Empty);

      //Assert
      using (new AssertionScope())
      {
        addOperationResult.Result.Should().Be(expectedResult);
        _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()), Times.Never);
      }
    }

    /// <summary>
    /// Test case: When the tracking id is sent the data is saved in the repository.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task SqrtElementsAsync_WhenElementsAreSentAndTrackingIdISent_RepositoryIsCalled()
    {
      //Arrange
      var addOperationDto = new OperationDTOOperands()
      {
        Operands = new List<int>() { 4 }
      };

      var trackingId = "someTrackingId";

      //Act 
      var addOperationResult = await _sut.SqrtElementsAsync(addOperationDto, trackingId);

      //Assert
      _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(
        It.Is<OperationDTO>(x => x.Calculation == "sqrt(4)=2" && x.Operation == "Sqr" && x.TrackingId == trackingId)),
        Times.Once);
    }

    public static IEnumerable<object[]> DataSqrt()
    {
      yield return new object[] { new List<int> { 16 }, 4 };
      yield return new object[] { new List<int> { 9 }, 3 };
    }
  }
}