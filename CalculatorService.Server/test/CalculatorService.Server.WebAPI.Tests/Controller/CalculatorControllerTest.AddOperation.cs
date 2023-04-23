using CalculatorService.Server.Core.Model.Entitites;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
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
    [MemberData(nameof(DataAdd))]
    public async Task AddElementsAsync_WhenAllTheDataIsOk_ReturnCorrectResult(List<int> elementsToBeAdded, int expectedResult)
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
        response.Result.Should().Be(expectedResult);
        //Validate the response no take more than 5 seconds to return the element.
        timer.ElapsedMilliseconds.Should().BeLessThanOrEqualTo(5000);
      }
    }

    [Fact]
    public async Task AddElementsAsync_WhenTheDataIsNotOk_Returns400StatusCode()
    {
      //Arrange
      var addOperationModel = new AddOperationModel()
      {
        Addends = new List<int>() { 1 }
      };

      StringContent bodyToBeSend = new(JsonSerializer.Serialize(addOperationModel), Encoding.UTF8, "application/json");

      //Act
      var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/add", bodyToBeSend);

      var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

      //Assert
      using(new AssertionScope()) 
      {
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.ErrorCode.Should().Be("InvalidRequest");
        response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
        response.ErrorMessage.Should().Be("Unable to process request: At least two elements should be provided");
      }
    }

    [Fact]
    public async Task AddElementsAsync_WhenSomeInternalIssueHappened_Returns500StatusCode()
    {
      //Arrange
      var addOperationModel = new AddOperationModel()
      {
        Addends = new List<int>() { 1,2 }
      };

      StringContent bodyToBeSend = new(JsonSerializer.Serialize(addOperationModel), Encoding.UTF8, "application/json");

      _testClient.HttpClient.DefaultRequestHeaders.Add(TrackingHeader, "X-Evi-Tracking-Id");

      _testClient.DataSourceMocks
                 .MockRepository
                 .Setup(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()))
                 .ThrowsAsync(new Exception());

      var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/add", bodyToBeSend);

      var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

      //Assert
      using (new AssertionScope())
      {
        httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        response.ErrorCode.Should().Be("InternalError");
        response.ErrorStatus.Should().Be((int)HttpStatusCode.InternalServerError);
        response.ErrorMessage.Should().Be("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support");
      }
    }


    public static IEnumerable<object[]> DataAdd()
    {
      yield return new object[] { new List<int> { -1, 3, -2 }, 0 };
      yield return new object[] { new List<int> { 1, 5, 7 }, 13 };
      yield return new object[] { new List<int> { 4, 5 }, 9 };
    }
  }
}
