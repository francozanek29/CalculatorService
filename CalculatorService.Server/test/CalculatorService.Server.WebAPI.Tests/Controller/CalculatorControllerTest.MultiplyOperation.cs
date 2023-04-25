namespace CalculatorService.Server.WebAPI.Tests.ControllerTests
{
    public partial class CalculatorControllerTest
    {
        /// <summary>
        /// Test Case: Validate the API working as expected when the data sent is correct under the 5 seconds 
        /// </summary>
        [Theory]
        [MemberData(nameof(DataMultiply))]
        public async Task MultiplyElementsAsync_WhenAllTheDataIsOk_ReturnCorrectResult(List<int> elementsToBeMultiplied, int expectedResult)
        {
            //Arrange
            var multiplyOperationModel = new MultiplyOperationModel()
            {
                Factors = elementsToBeMultiplied,
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(multiplyOperationModel), Encoding.UTF8, "application/json");

            //Act
            var timer = new Stopwatch();

            timer.Start();

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/mult", bodyToBeSend);

            timer.Stop();

            var response = JsonSerializer.Deserialize<MultiplyOperationResultModel>(await httpResponse.Content.ReadAsStringAsync());

            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Result.Should().Be(expectedResult);
                //Validate the response no take more than 5 seconds to return the element.
                timer.ElapsedMilliseconds.Should().BeLessThanOrEqualTo(5000);
            }
        }

        /// <summary>
        /// Test Case: When the input body is not ok, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task MultiplyElementsAsync_WhenTheDataIsNotOk_Returns400StatusCode()
        {
            //Arrange
            var multiplyOperationModel = new MultiplyOperationModel()
            {
                Factors = new List<int>() { 1 }
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(multiplyOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/mult", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: At least two elements should be provided");
            }
        }

        /// <summary>
        /// Test Case: When there is an internal error, the response is 500 Internal Server Error.
        /// </summary>
        [Fact]
        public async Task MultiplyElementsAsync_WhenSomeInternalIssueHappened_Returns500StatusCode()
        {
            //Arrange
            var multiplyOperationModel = new MultiplyOperationModel()
            {
                Factors = new List<int>() { 1, 2 }
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(multiplyOperationModel), Encoding.UTF8, "application/json");

            _testClient.HttpClient.DefaultRequestHeaders.Add(TrackingHeader, "X-Evi-Tracking-Id");

            _testClient.DataSourceMocks
                       .MockRepository
                       .Setup(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()))
                       .ThrowsAsync(new Exception());

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/mult", bodyToBeSend);

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


        public static IEnumerable<object[]> DataMultiply()
        {
            yield return new object[] { new List<int> { -1, 3, -2 }, 6 };
            yield return new object[] { new List<int> { -1, 5, 7 }, -35 };
            yield return new object[] { new List<int> { 0, 5000000 }, 0 };
        }
    }
}
