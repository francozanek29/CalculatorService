using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorService.Server.WebAPI.Tests.Controller
{
    public class JournalControllerTests : IClassFixture<TestApplication>
    {
        private readonly TestApplication _testApplication;

        private const string TrackingHeader = "X-Evi-Tracking-Id";

        private const string TrackingHeaderValue = "someValue";
        private const string AnotherTrackingHeaderValue = "anotherValue";

        public JournalControllerTests(TestApplication testApplication)
        {
            _testApplication = testApplication;
        }



        [Fact] 
        public async Task WhenNonOperationWasSent_ReturnEmptyList()
        {
            //Arrange

            var testClient = _testApplication.SetupTestRequest();

            var journalModel = new JournalModel()
            {
                TrackingId = "someTrackingId"
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(journalModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await testClient.HttpClient.PostAsync("/journal/query", bodyToBeSend);

            var response = JsonSerializer.Deserialize<JournalModelResult>(await httpResponse.Content.ReadAsStringAsync());

            //Arrange
            using(new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Operations.Count().Should().Be(0);
            }
        }

        [Fact]
        public async Task WhenOneOperationWasSent_ReturnOneOperation()
        {
            //Arrange

            var testClient = _testApplication.SetupTestRequest(service =>
            {
                service.AddScoped<IRepository, JournalRepository>();
            });

            var addOperationModel = new AddOperationModel()
            {
                Addends = new List<int>() { 1, 2 }
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(addOperationModel), Encoding.UTF8, "application/json");

            testClient.HttpClient.DefaultRequestHeaders.Add(TrackingHeader, TrackingHeaderValue);

            var httpResponse = await testClient.HttpClient.PostAsync("/calculator/add", bodyToBeSend);

            var journalModel = new JournalModel()
            {
                TrackingId = TrackingHeaderValue
            };

            StringContent bodyToBeSendJournal = new(JsonSerializer.Serialize(journalModel), Encoding.UTF8, "application/json");

            //Act

            var httpResponseJournal = await testClient.HttpClient.PostAsync("/journal/query", bodyToBeSendJournal);

            var response = JsonSerializer.Deserialize<JournalModelResult>(await httpResponseJournal.Content.ReadAsStringAsync());

            //Arrange
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                httpResponseJournal.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Operations.Count().Should().Be(1);

                var firstOperation = response.Operations.FirstOrDefault();
                firstOperation.Calculation.Should().Be("1 + 2 = 3");
                firstOperation.Operation.Should().Be("Sum");
            }
        }

        [Fact]
        public async Task WhenTwoOperationWasSent_ReturnTwoOperations()
        {
            //Arrange

            var testClient = _testApplication.SetupTestRequest(service =>
            {
                service.AddScoped<IRepository, JournalRepository>();
            });

            var addOperationModel = new AddOperationModel()
            {
                Addends = new List<int>() { 1, 2 }
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(addOperationModel), Encoding.UTF8, "application/json");

            testClient.HttpClient.DefaultRequestHeaders.Add(TrackingHeader, AnotherTrackingHeaderValue);

            var httpResponseFirstOperation = await testClient.HttpClient.PostAsync("/calculator/add", bodyToBeSend);
            var httpResponseSecondOperation = await testClient.HttpClient.PostAsync("/calculator/add", bodyToBeSend);

            var journalModel = new JournalModel()
            {
                TrackingId = AnotherTrackingHeaderValue
            };

            StringContent bodyToBeSendJournal = new(JsonSerializer.Serialize(journalModel), Encoding.UTF8, "application/json");

            //Act

            var httpResponseJournal = await testClient.HttpClient.PostAsync("/journal/query", bodyToBeSendJournal);

            var response = JsonSerializer.Deserialize<JournalModelResult>(await httpResponseJournal.Content.ReadAsStringAsync());

            //Arrange
            using (new AssertionScope())
            {
                httpResponseFirstOperation.StatusCode.Should().Be(HttpStatusCode.OK);
                httpResponseSecondOperation.StatusCode.Should().Be(HttpStatusCode.OK);
                httpResponseJournal.StatusCode.Should().Be(HttpStatusCode.OK);
                response!.Operations.Count().Should().Be(2);

                var firstOperation = response.Operations.FirstOrDefault();
                firstOperation!.Calculation.Should().Be("1 + 2 = 3");
                firstOperation!.Operation.Should().Be("Sum");
                
                var secondOperation = response.Operations.FirstOrDefault();
                secondOperation!.Calculation.Should().Be("1 + 2 = 3");
                secondOperation!.Operation.Should().Be("Sum");
            }
        }

        [Fact]
        public async Task WhenTheDataIsNotOk_Returns400StatusCode()
        {
            //Arrange
            var testClient = _testApplication.SetupTestRequest();

            var journalModel = new JournalModel();
            StringContent bodyToBeSend = new(JsonSerializer.Serialize(journalModel), Encoding.UTF8, "application/json");

            //Act
            var httpResponse = await testClient.HttpClient.PostAsync("/journal/query", bodyToBeSend);

            var response = JsonSerializer.Deserialize<ErrorDescriptionClass>(await httpResponse.Content.ReadAsStringAsync());

            //Assert
            using (new AssertionScope())
            {
                httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.ErrorCode.Should().Be("InvalidRequest");
                response.ErrorStatus.Should().Be((int)HttpStatusCode.BadRequest);
                response.ErrorMessage.Should().Be("Unable to process request: The TrackingId field is required.");
            }
        }

        [Fact]
        public async Task WhenSomeInternalIssueHappened_Returns500StatusCode()
        {
            //Arrange
            var testClient = _testApplication.SetupTestRequest();

            var journalModel = new JournalModel()
            {
                TrackingId = "someTrackingId"
            };

            StringContent bodyToBeSend = new(JsonSerializer.Serialize(journalModel), Encoding.UTF8, "application/json");

            testClient.DataSourceMocks
                       .MockRepository
                       .Setup(mr => mr.GetAllOperationInfosByTrackingIdAsync(It.IsAny<string>()))
                       .ThrowsAsync(new Exception());

            var httpResponse = await testClient.HttpClient.PostAsync("/journal/query", bodyToBeSend);

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
    }
}
