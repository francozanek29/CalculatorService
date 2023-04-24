namespace CalculatorService.Server.Core.Services.Tests
{
  public partial class CalculatorServicesTests
  {
    /// <summary>
    /// Test cases: When the elements to be added are sent, the result is correct, and not tracking Id is sent,
    /// the repository is not being called.
    /// </summary>
    /// <param name="elementsToBeDivided"></param>
    /// <param name="expectedResult"></param>
    /// <returns></returns>
    [Theory]
    [MemberData(nameof(DataDiv))]
    public async Task DivElementsAsync_WhenElementsAreSentAndNotTrackingIdISent_ReturnsSum(List<int> elementsToBeDivided, int expectedResultQuotient, int expectedResultRemainder)
    {
      //Arrange
      var divOperationDto = new OperationDTOOperands()
      {
        Operands = elementsToBeDivided
      };

      //Act 
      var divOperationResult = (OperationResultDTOExtension) await _sut.DivElementsAsync(divOperationDto, string.Empty);

      //Assert
      using (new AssertionScope())
      {
        divOperationResult.Result.Should().Be(expectedResultQuotient);
        divOperationResult.ExtraResult.Should().Be(expectedResultRemainder);
        _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()), Times.Never);
      }
    }

    /// <summary>
    /// Test case: When the tracking id is sent the data is saved in the repository.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DivElementsAsync_WhenElementsAreSentAndTrackingIdISent_RepositoryIsCalled()
    {
      //Arrange
      var divOperationDto = new OperationDTOOperands()
      {
        Operands = new List<int>() { 2, 3 }
      };

      var trackingId = "someTrackingId";

      //Act 
      var addOperationResult = await _sut.DivElementsAsync(divOperationDto, trackingId);

      //Assert
      _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(
        It.Is<OperationDTO>(x => x.Calculation == "2/3=0-Remainder=2" && x.Operation == "Div" && x.TrackingId == trackingId)),
        Times.Once);
    }

    public static IEnumerable<object[]> DataDiv()
    {
      yield return new object[] { new List<int> { 0, 6 }, 0, 0 };
      yield return new object[] { new List<int> { 4, 5 }, 0, 4 };
      yield return new object[] { new List<int> { 12, 6 }, 2, 0 };
      yield return new object[] { new List<int> { 10, 6 }, 1, 4 };
    }
  }
}