using System.Text.Json.Serialization;

namespace GitHubConnectorAPI.Models
{
    public class CreateMilestoneRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        // must be "open" or "closed"
        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
