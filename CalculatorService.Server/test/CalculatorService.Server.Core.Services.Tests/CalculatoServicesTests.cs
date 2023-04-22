namespace CalculatorService.Server.Core.Services.Tests
{
  public class CalculatoServicesTests
  {
    private readonly CalculatorServices _sut = new();

    /// <summary>
    /// Test cases: When the elements to be added are sent, the result is correct.
    /// </summary>
    /// <param name="elementsToBeAdded"></param>
    /// <param name="expectedResult"></param>
    /// <returns></returns>
    [Theory]
    [MemberData(nameof(Data))]
    public async Task AddElementsAsync_WhenElementsAreSent_ReturnsSum(List<int> elementsToBeAdded, int expectedResult)
    {
      //Arrange
      var addOperationDto = new AddOperationDTO()
      {
        Addends = elementsToBeAdded
      };

      //Act 
      var addOperationResult = await _sut.AddElementsAsync(addOperationDto);

      //Assert
      addOperationResult.Sum.Should().Be(expectedResult);
    }

    public static IEnumerable<object[]> Data()
    {
      yield return new object[] { new List<int> { -1,3,-2 }, 0};
      yield return new object[] { new List<int> { 1,5,7},13 };
      yield return new object[] { new List<int> { 4,5 }, 9};
    }
  }
}