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
    public async Task AddElementsAsync_WhenElementsAreSentAndNotTrackingIdISent_ReturnsSum(List<int> elementsToBeAdded, int expectedResult)
    {
      //Arrange
      var addOperationDto = new OperationDTOOperands()
      {
        Operands = elementsToBeAdded
      };

      _sut = new CalculatorServices(_mockRepository.Object, _requestContextForNoTracking, _mockLogger.Object);

      //Act 
      var addOperationResult = await _sut.ExecuteOperation(addOperationDto, ValidOperations.Sum);

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
    public async Task AddElementsAsync_WhenElementsAreSentAndTrackingIdISent_RepositoryIsCalled()
    {
      //Arrange
      var addOperationDto = new OperationDTOOperands()
      {
        Operands = new List<int>() { 2, 3 }
      };

      _sut = new CalculatorServices(_mockRepository.Object, _requestContextForTracking, _mockLogger.Object);

      //Act 
      var addOperationResult = await _sut.ExecuteOperation(addOperationDto, ValidOperations.Sum);

      //Assert
      _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(
        It.Is<OperationDTO>(x => x.Calculation == "2 + 3 = 5" && x.Operation == "Sum" && x.TrackingId == _requestContextForTracking.TrackingId)),
        Times.Once);
    }

    public static IEnumerable<object[]> DataAdd()
    {
      yield return new object[] { new List<int> { -1, 3, -2 }, 0 };
      yield return new object[] { new List<int> { 1, 5, 7 }, 13 };
      yield return new object[] { new List<int> { 4, 5 }, 9 };
    }
  }
}