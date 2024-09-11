
using MercadoLivre.Core.Interface.IServices;
using MercadoLivre.Core.Models.MLMessage;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using MercadoLivreTeste.App.Models.MLQuestions;
using MercadoLivre.Core.Models.MLQuestion;

namespace MercadoLivre.Core.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetResourceAsync(string resource, string accessToken)
        {         
            HttpClient client = _httpClientFactory.CreateClient("mercadolivre.api");

            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            message.Method = HttpMethod.Get;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                message.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            message.RequestUri = new Uri($"https://api.mercadolibre.com{resource}");

            HttpResponseMessage apiResponse = await client.SendAsync(message);

            HttpStatusCode statusCode = apiResponse.StatusCode;
            string apiContent = await apiResponse.Content.ReadAsStringAsync();

            // must log when status code is bedrequest

            return apiContent;
        }

        public async Task<string> SendAnswerToMercadoLivreAsync(string jsonContent, string resource, string accessToken)
        {
            HttpClient client = _httpClientFactory.CreateClient("mercadolivre.api");

            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            message.Method = HttpMethod.Post;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                message.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            // packs/orderid/sellers/userid
            message.RequestUri = new Uri($"https://api.mercadolibre.com{resource}");

            message.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage apiResponse = await client.SendAsync(message);

            HttpStatusCode statusCode = apiResponse.StatusCode;
            string apiContent = await apiResponse.Content.ReadAsStringAsync();

            // must log when statusCode is badrequest

            return apiContent;
        }

        public async Task<string> SendAnswerToMercadoLivreQuestionAsync(MLAnswerToQuestion answer, string accessToken)
        {
            HttpClient client = _httpClientFactory.CreateClient("mercadolivre.api");

            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            message.Method = HttpMethod.Post;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                message.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            string uri = $"https://api.mercadolibre.com/answers";
            message.RequestUri = new Uri(uri);

            string jsonContent = JsonSerializer.Serialize(answer);
            message.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage apiResponse = await client.SendAsync(message);

            HttpStatusCode statusCode = apiResponse.StatusCode;
            string apiContent = await apiResponse.Content.ReadAsStringAsync();

            // must log when statusCode is badrequest

            return apiContent;
        }
    }
}
