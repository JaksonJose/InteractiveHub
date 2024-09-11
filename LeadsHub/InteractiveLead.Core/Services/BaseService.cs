using System.Net;
using System.Text.Json;
using System.Text;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Interfaces.IServices;

namespace InteractiveLead.Core.Services
{
    public sealed class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BaseResponse> SendMessageAsync(string messageSerielized, string uri)
        {
            BaseResponse response = new();

            try
            {
                HttpClient client = _httpClientFactory.CreateClient("leadsManager");

                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //message.Headers.Add("Authorization", $"Bearer {TempToken}");
                message.Method = HttpMethod.Post;

                message.RequestUri = new Uri(uri);

                message.Content = new StringContent(messageSerielized, Encoding.UTF8, "application/json");

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                HttpStatusCode statusCode = apiResponse.StatusCode;
                string apiContent = await apiResponse.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                response = JsonSerializer.Deserialize<BaseResponse>(apiContent, options) ?? new();
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }
    }
}
