namespace CalculatorService.Server.Core.Services.Tests
{
  public partial class CalculatorServicesTests
  {
    /// <summary>
    /// Test cases: When the elements to be used in the calculation, the result is correct, and not tracking Id is sent,
    /// the repository is not being called.
    /// </summary>
    /// <param name="elementsToBeAdded"></param>
    /// <param name="expectedResult"></param>
    /// <returns></returns>
    [Theory]
    [MemberData(nameof(DataSqrt))]
    public async Task SqrtElementsAsync_WhenElementsAreSentAndNotTrackingIdISent_ReturnsSum(List<int> elementsToBeUsed, int expectedResult)
    {
      //Arrange
      var elementOperationDto = new OperationDTOOperands()
      {
        Operands = elementsToBeUsed
      };

      _sut = new CalculatorServices(_mockRepository.Object, _requestContextForNoTracking);

      //Act 
      var addOperationResult = await _sut.SqrtElementsAsync(elementOperationDto);

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
      var elementOperationDto = new OperationDTOOperands()
      {
        Operands = new List<int>() { 4 }
      };

      _sut = new CalculatorServices(_mockRepository.Object, _requestContextForTracking);

      //Act 
      var addOperationResult = await _sut.SqrtElementsAsync(elementOperationDto);

      //Assert
      _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(
        It.Is<OperationDTO>(x => x.Calculation == "sqrt(4) = 2" && x.Operation == "Sqr" && x.TrackingId == _requestContextForTracking.TrackingId)),
        Times.Once);
    }

    public static IEnumerable<object[]> DataSqrt()
    {
      yield return new object[] { new List<int> { 16 }, 4 };
      yield return new object[] { new List<int> { 9 }, 3 };
    }
  }
}