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
        [Theory]
        [MemberData(nameof(DataSqrt))]
        public async Task SqrtElementsAsync_WhenAllTheDataIsOk_ReturnCorrectResult(int elementsToBeUsed, int expectedResult)
        {
            //Arrange
            var sqrtOperationModel = new SqrtOperationModel()
            {
                Number = elementsToBeUsed,
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(sqrtOperationModel), Encoding.UTF8, "application/json");

            //Act
            var timer = new Stopwatch();

            timer.Start();

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sqrt", bodyToBeSend);

            timer.Stop();

            var response = JsonSerializer.Deserialize<SqrtOperationResultModel>(await httpResponse.Content.ReadAsStringAsync());

            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Result.Should().Be(expectedResult);
                //Validate the response no take more than 5 seconds to return the element.
                timer.ElapsedMilliseconds.Should().BeLessThanOrEqualTo(5000);
            }
        }

        /// <summary>
        /// Test Case: When the input value is Zero or negative, the response is 400 Bad Request.
        /// </summary>
        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        public async Task SqrtElementsAsync_WhenTheDataIsNotOk_Returns400StatusCode(int number)
        {
            //Arrange
            var sqrtOperationModel = new SqrtOperationModel()
            {
                Number = number,
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(sqrtOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sqrt", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The number should be greater thatn zero");
            }
        }

        /// <summary>
        /// Test Case: When there is an internal error, the response is 500 Internal Server Error.
        /// </summary>
        [Fact]
        public async Task SqrtElementsAsync_WhenSomeInternalIssueHappened_Returns500StatusCode()
        {
            //Arrange
            var sqrtOperationModel = new SqrtOperationModel()
            {
                Number = 16,
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(sqrtOperationModel), Encoding.UTF8, "application/json");

            _testClient.HttpClient.DefaultRequestHeaders.Add(TrackingHeader, "X-Evi-Tracking-Id");

            _testClient.DataSourceMocks
                       .MockRepository
                       .Setup(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()))
                       .ThrowsAsync(new Exception());

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sqrt", bodyToBeSend);

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


        public static IEnumerable<object[]> DataSqrt()
        {
            yield return new object[] { 16, 4 };
            yield return new object[] { 9, 3 };
        }
    }
}
