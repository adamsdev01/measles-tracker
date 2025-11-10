using measles_tracker.app.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace measles_tracker.app.Services
{
    public class GithubRepoService
    {
        private readonly HttpClient _http;

        public GithubRepoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DateTime?> GetRepoLastUpdatedAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.github.com/repos/CSSEGISandData/measles_data"
            );

            // GitHub API requires a User-Agent header
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("BlazorApp", "1.0"));

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            var repo = JsonSerializer.Deserialize<GitHubRepoInfo>(json);

            return DateTime.Parse(repo.updated_at);
        }
    }
}
