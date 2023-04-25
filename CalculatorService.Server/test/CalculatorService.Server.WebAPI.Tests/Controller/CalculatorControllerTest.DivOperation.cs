namespace CalculatorService.Server.WebAPI.Tests.ControllerTests
{
    public partial class CalculatorControllerTest
    {
        /// <summary>
        /// Test Case: Validate the API working as expected when the data sent is correct under the 5 seconds 
        /// </summary>
        [Theory]
        [MemberData(nameof(DataDiv))]
        public async Task DivElementsAsync_WhenAllTheDataIsOk_ReturnCorrectResult(int dividend, int divisor, int expectedQuotient, int expectedRemainder)
        {
            //Arrange
            var divOperationModel = new DivOperationModel()
            {
                Dividend = dividend,
                Divisor = divisor
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(divOperationModel), Encoding.UTF8, "application/json");

            //Act
            var timer = new Stopwatch();

            timer.Start();

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/div", bodyToBeSend);

            timer.Stop();

            var response = JsonSerializer.Deserialize<DivOperationResultModel>(await httpResponse.Content.ReadAsStringAsync());

            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Result.Should().Be(expectedQuotient);
                response.Remainder.Should().Be(expectedRemainder);
                //Validate the response no take more than 5 seconds to return the element.
                timer.ElapsedMilliseconds.Should().BeLessThanOrEqualTo(5000);
            }
        }

        /// <summary>
        /// Test Case: When the Divisor is missing, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task DivElementsAsync_WhenDivisorIsMissing_Returns400StatusCode()
        {
            //Arrange
            var divOperationModel = new DivOperationModel()
            {
                Dividend = 3
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(divOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/div", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The Divisor field is required.");
            }
        }

        /// <summary>
        /// Test Case: When the Dividend is missing, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task DivElementsAsync_WhenDividendIsMissing_Returns400StatusCode()
        {
            //Arrange
            var divOperationModel = new DivOperationModel()
            {
                Divisor = 3
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(divOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/div", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The Dividend field is required.");
            }
        }


        /// <summary>
        /// Test Case: When both fields are missing, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task DivElementsAsync_WhenBothFieldsAreMissing_Returns400StatusCode()
        {
            //Arrange
            var divOperationModel = new DivOperationModel();

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(divOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/div", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The Divisor field is required.,The Dividend field is required.");
            }
        }

        /// <summary>
        /// Test Case: When the Divisor is zero, the response is 400 Bad Request.
        /// </summary>
        [Fact]
        public async Task DivElementsAsync_WhenDivisorIsZero_Returns400StatusCode()
        {
            //Arrange
            var divOperationModel = new DivOperationModel()
            {
                Dividend = 3,
                Divisor = 0
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(divOperationModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/div", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The Divisor should not be zero");
            }
        }

        /// <summary>
        /// Test Case: When there is an internal error, the response is 500 Internal Server Error.
        /// </summary>
        [Fact]
        public async Task DivElementsAsync_WhenSomeInternalIssueHappened_Returns500StatusCode()
        {
            //Arrange
            var divOperationModel = new DivOperationModel()
            {
                Dividend = 3,
                Divisor = 4
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(divOperationModel), Encoding.UTF8, "application/json");

            _testClient.HttpClient.DefaultRequestHeaders.Add(TrackingHeader, "X-Evi-Tracking-Id");

            _testClient.DataSourceMocks
                       .MockRepository
                       .Setup(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()))
                       .ThrowsAsync(new Exception());

            var httpResponse = await _testClient.HttpClient.PostAsync("/calculator/div", bodyToBeSend);

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
        public static IEnumerable<object[]> DataDiv()
        {
            yield return new object[] { 0, 6, 0, 0 };
            yield return new object[] { 4, 5, 0, 4 };
            yield return new object[] { 12, 6, 2, 0 };
            yield return new object[] { 10, 6, 1, 4 };
        }
    }
}
