using System.Text.Json;
using System.Text;

namespace GitHubConnectorAPI.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly string _pat;

        public GitHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _pat = configuration["GitHub:Token"];

            if (string.IsNullOrWhiteSpace(_pat))
                throw new InvalidOperationException("GITHUB_PAT must be set");

            // Clear existing headers and set required GitHub headers
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"token {_pat}"); // token auth
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubConnectorApp"); // required by GitHub
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");

            // Ensure BaseAddress is set
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://api.github.com/");
            }
        }

        public async Task<string> GetRepositories(string? owner = null)
        {
            HttpResponseMessage resp;

            if (string.IsNullOrWhiteSpace(owner))
            {
                resp = await _httpClient.GetAsync("user/repos");
            }
            else
            {
                resp = await _httpClient.GetAsync($"users/{Uri.EscapeDataString(owner)}/repos");
            }

            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new ApiException((int)resp.StatusCode, body);

            return body;
        }

        public async Task<string> ListIssues(string owner, string repo)
        {
            var resp = await _httpClient.GetAsync($"repos/{Uri.EscapeDataString(owner)}/{Uri.EscapeDataString(repo)}/issues");
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new ApiException((int)resp.StatusCode, body);

            return body;
        }

        public async Task<string> ListMilestones(string owner, string repo)
        {
            var resp = await _httpClient.GetAsync($"repos/{Uri.EscapeDataString(owner)}/{Uri.EscapeDataString(repo)}/milestones");
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new ApiException((int)resp.StatusCode, body);

            return body;
        }

        public async Task<string> ListCommits(string owner, string repo)
        {
            var url = $"repos/{Uri.EscapeDataString(owner)}/{Uri.EscapeDataString(repo)}/commits";
            var resp = await _httpClient.GetAsync(url);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new ApiException((int)resp.StatusCode, body);

            return body;
        }

        public async Task<string> CreateIssue(string owner, string repo, Models.CreateIssueRequest request)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(request, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"repos/{Uri.EscapeDataString(owner)}/{Uri.EscapeDataString(repo)}/issues";

            var resp = await _httpClient.PostAsync(url, content);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new ApiException((int)resp.StatusCode, body);

            return body;
        }

        public async Task<string> CreateMilestone(string owner, string repo, Models.CreateMilestoneRequest request)
        {
            // Ensure state is lowercase as GitHub expects "open" or "closed"
            if (!string.IsNullOrEmpty(request.State))
            {
                request.State = "open";
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(request, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"repos/{Uri.EscapeDataString(owner)}/{Uri.EscapeDataString(repo)}/milestones";

            var resp = await _httpClient.PostAsync(url, content);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new ApiException((int)resp.StatusCode, body);

            return body;
        }
    }
}
