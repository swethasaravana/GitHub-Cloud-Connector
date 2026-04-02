namespace GitHubConnectorAPI.Models
{
    public class CreateIssueRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Body { get; set; }
        public int? Milestone { get; set; }
    }
}
