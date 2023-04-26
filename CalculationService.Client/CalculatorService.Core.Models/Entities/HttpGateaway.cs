using System.Net;
using System.Text;
using System.Text.Json;
using CalculatorService.Core.Models.Entities.ResponseModels;

namespace CalculatorService.Core.Models.Entities
{
    public class HttpGateaway
    {
        private readonly HttpClient _httpClient = new();

        public async Task PostAsyncAndShow<T>(string url, string trackingId, T modelToBeSend)
        {
            StringContent bodyToBeSend = new(JsonSerializer.Serialize(modelToBeSend), Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(trackingId))
            {
                _httpClient.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", trackingId);
            }

            var response = await _httpClient.PostAsync(url, bodyToBeSend);

            await ShowResponse(response);
        }

        private async Task ShowResponse(HttpResponseMessage response)
        {
            var httpCode = (int)response.StatusCode;

            var responseAsString = await response.Content.ReadAsStringAsync();

            if (httpCode >= 400) //Errors & Faults
            {
                var responseError = JsonSerializer.Deserialize<ErrorResponseModel>(responseAsString);

                Console.WriteLine("There was an error during the execution of the request.");
                Console.WriteLine($"Status Code: {responseError.ErrorCode}");
                Console.WriteLine($"Status Message: {responseError.ErrorMessage}");
            }
            else
            {
                Console.WriteLine("The operation was ok");
                Console.WriteLine(responseAsString);
            }
        }
    }
}
