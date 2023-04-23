namespace CalculatorService.Server.Core.Services.Tests
{
  public partial class CalculatorServicesTests
  {
    /// <summary>
    /// Test cases: When the elements to be multiplied are sent, the result is correct, and not tracking Id is sent,
    /// the repository is not being called.
    /// </summary>
    /// <param name="elementsToBeMultiplied"></param>
    /// <param name="expectedResult"></param>
    /// <returns></returns>
    [Theory]
    [MemberData(nameof(DataMultiply))]
    public async Task MultiplyElementsAsync_WhenElementsAreSentAndNotTrackingIdISent_ReturnsSum(List<int> elementsToBeMultiplied, int expectedResult)
    {
      //Arrange
      var addOperationDto = new OperationDTOOperands()
      {
        Operands = elementsToBeMultiplied
      };

      //Act 
      var addOperationResult = await _sut.MultiplyElementsAsync(addOperationDto, string.Empty);

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
    public async Task MultiplyElementsAsync_WhenElementsAreSentAndTrackingIdISent_RepositoryIsCalled()
    {
      //Arrange
      var multiplyOperationDto = new OperationDTOOperands()
      {
        Operands = new List<int>() { 2, 3 }
      };

      var trackingId = "someTrackingId";

      //Act 
      var addOperationResult = await _sut.MultiplyElementsAsync(multiplyOperationDto, trackingId);

      //Assert
      _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(
        It.Is<OperationDTO>(x => x.Calculation == "2*3=6" && x.Operation == "Mul" && x.TrackingId == trackingId)),
        Times.Once);
    }

    public static IEnumerable<object[]> DataMultiply()
    {
      yield return new object[] { new List<int> { -1, 3, -2 }, 6 };
      yield return new object[] { new List<int> { -1, 5, 7 }, -35 };
      yield return new object[] { new List<int> { 0, 500000000 }, 0 };
    }
  }
}
