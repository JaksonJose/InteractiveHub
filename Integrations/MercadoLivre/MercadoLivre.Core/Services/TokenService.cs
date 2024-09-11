

using MercadoLivre.Core.Interface.IServices;
using MercadoLivre.Core.Models;
using MercadoLivre.Core.Models.MLRefreshToken;
using System.Net;
using System.Text.Json;

namespace MercadoLivre.Core.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MLRefreshToken> RefreshTokenAsync(MercadoLivreConfig config)
        {
            MLRefreshToken response = new();

            try
            {
                HttpClient client = _httpClientFactory.CreateClient("mercadolivre.api");

                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.Method = HttpMethod.Post;

                // Create form data
                var formData = new Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", config.ApplicationId.ToString() },
                    { "client_secret", config.ClientSecret },
                    { "refresh_token", config.RefreshToken }
                };

                message.RequestUri = new Uri($"https://api.mercadolibre.com/oauth/token");

                message.Content = new FormUrlEncodedContent(formData);

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                HttpStatusCode statusCode = apiResponse.StatusCode;
                string apiContent = await apiResponse.Content.ReadAsStringAsync();

                // TODO: implement error treatment

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                response = JsonSerializer.Deserialize<MLRefreshToken>(apiContent, options) ?? new();
            }
            catch (Exception ex)
            {
                //TODO: Should save a log
            }

            return response;
        }
    }
}
