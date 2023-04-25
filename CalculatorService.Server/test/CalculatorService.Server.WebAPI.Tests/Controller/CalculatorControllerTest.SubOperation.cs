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
        [MemberData(nameof(DataSub))]
        public async Task SubElementsAsync_WhenAllTheDataIsOk_ReturnCorrectResult(int minuend, int subtrahend, int expectedResult)
        {
            //Arrange
            var subOperationModel = new SubOperationModel()
            {
                Minuend = minuend,
                Subtrahend = subtrahend
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(subOperationModel), Encoding.UTF8, "application/json");

            //Act
            var timer = new Stopwatch();

            timer.Start();

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sub", bodyToBeSend);

            timer.Stop();

            var response = JsonSerializer.Deserialize<SubOperationResultModel>(await httpResponse.Content.ReadAsStringAsync());

            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Result.Should().Be(expectedResult);
                //Validate the response no take more than 5 seconds to return the element.
                timer.ElapsedMilliseconds.Should().BeLessThanOrEqualTo(5000);
            }
        }


        /// <summary>
        /// Test Case: When the Minuend is missing, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task SubElementsAsync_WhenMinuendIsMissing_Returns400StatusCode()
        {
            //Arrange
            var subOperationModel = new SubOperationModel()
            {
                Subtrahend = 3
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(subOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sub", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The Minuend field is required.");
            }
        }

        /// <summary>
        /// Test Case: When the Subtrahendd is missing, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task SubElementsAsync_WhenSubtrahenddIsMissing_Returns400StatusCode()
        {
            //Arrange
            var subOperationModel = new SubOperationModel()
            {
                Minuend = 3
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(subOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sub", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The Subtrahend field is required.");
            }
        }

        /// <summary>
        /// Test Case: When both fields, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task SubElementsAsync_WhenBothFieldsAreMissing_Returns400StatusCode()
        {
            //Arrange
            var subOperationModel = new SubOperationModel();

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(subOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sub", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The Minuend field is required.,The Subtrahend field is required.");
            }
        }

        /// <summary>
        /// Test Case: When there is an internal error, the response is 500 Internal Server Error.
        /// </summary>
        [Fact]
        public async Task SubElementsAsync_WhenSomeInternalIssueHappened_Returns500StatusCode()
        {
            //Arrange
            var subOperationModel = new SubOperationModel()
            {
                Minuend = 3,
                Subtrahend = 2
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(subOperationModel), Encoding.UTF8, "application/json");

            _testClient.HttpClient.DefaultRequestHeaders.Add(TrackingHeader, "X-Evi-Tracking-Id");

            _testClient.DataSourceMocks
                       .MockRepository
                       .Setup(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()))
                       .ThrowsAsync(new Exception());

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/sub", bodyToBeSend);

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


        public static IEnumerable<object[]> DataSub()
        {
            yield return new object[] { -1, 3, -4 };
            yield return new object[] { 1, 1, 0 };
            yield return new object[] { 5, 4, 1 };
        }
    }
}
