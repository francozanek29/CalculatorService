using FluentAssertions;
using FluentAssertions.Execution;
using System.Diagnostics;
using System.Net;

namespace CalculatorService.Server.WebAPI.Tests.ControllerTests
{
  public partial class CalculatorControllerTest
  {
    /// <summary>
    /// Test Case: Validate the API working as expected when the data sent is correct under the 5 seconds 
    /// </summary>
    /// <param name="elementsToBeAdded"></param>
    /// <param name="expectedResult"></param>
    /// <returns></returns>
    [Theory]
    [MemberData(nameof(Data))]
    public async Task WhenAllTheDataIsOk_ReturnCorrectResult(List<int> elementsToBeAdded, int expectedResult)
    {
      //Arrange
      var addOperationModel = new AddOperationModel()
      {
        Addends = elementsToBeAdded,
      };

      StringContent bodyToBeSend = new(JsonSerializer.Serialize(addOperationModel), Encoding.UTF8, "application/json");

      //Act
      var timer = new Stopwatch();

      timer.Start();

      var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/add", bodyToBeSend);

      timer.Stop();

      var response = JsonSerializer.Deserialize<AddOperationResultModel>(await httpResponse.Content.ReadAsStringAsync());

      using (new AssertionScope())
      {
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Sum.Should().Be(expectedResult);
        //Validate the response no take more than 5 seconds to return the element.
        timer.ElapsedMilliseconds.Should().BeLessThanOrEqualTo(5000);
      }
    }

    
    public static IEnumerable<object[]> Data()
    {
      yield return new object[] { new List<int> { -1, 3, -2 }, 0 };
      yield return new object[] { new List<int> { 1, 5, 7 }, 13 };
      yield return new object[] { new List<int> { 4, 5 }, 9 };
    }
  }
}
