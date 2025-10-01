using LimeSurveyAPI.Configuration;
using LimeSurveyAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace LimeSurveyAPI.Services
{
    public class LimeSurveyService : ILimeSurveyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public LimeSurveyService(HttpClient httpClient, IConfiguration configuration, IOptions<LimeSurveySettings> settings)
        {
            _httpClient = httpClient;
            _apiUrl = settings.Value.ApiUrl ?? throw new Exception("LimeSurvey:ApiUrl not configured");
        }

        private async Task<T> CallApiAsync<T>(string method, params object[] parameters)
        {
            var request = new
            {
                method,
                id = 1,
                @params = parameters
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("error", out var error) && error.ValueKind != JsonValueKind.Null)
            {
                throw new Exception($"API Error: {error}");
            }
            return JsonSerializer.Deserialize<T>(doc.RootElement.GetProperty("result"))!;
        }

        // 🔑 Oturum açma
        public async Task<string> GetSessionKeyAsync(string username, string password)
        {
            return await CallApiAsync<string>("get_session_key", username, password);
        }

        // 🔹 Tüm kullanıcıları listeleme
        public Task<List<Dictionary<string, object>>> GetUsersAsync(string sessionKey)
        {
            return CallApiAsync<List<Dictionary<string, object>>>("list_users", sessionKey);
        }

    }
}
