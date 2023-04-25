using System.Net;
using System.Text;
using System.Text.Json;

namespace CalculatoService.Client
{
  public class HttpGateaway
  {
    private readonly HttpClient _httpClient = new();

    public async Task PostAsyncAndDeserialize<T>(string url,string trackingId, T modelToBeSend) 
    {
      StringContent bodyToBeSend = new(JsonSerializer.Serialize(modelToBeSend), Encoding.UTF8, "application/json");

      if (!string.IsNullOrEmpty(trackingId))
      {
        _httpClient.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", trackingId);
      }

      var response = await _httpClient.PostAsync(url,bodyToBeSend);

      var responseAsString = await response.Content.ReadAsStringAsync();

      return JsonSerializer.Deserialize<T>(responseAsString);
    }

    private void ShowResponse(string responseAsString, HttpStatusCode httpStatusCode) 
    { 
      
    }
  }
}
