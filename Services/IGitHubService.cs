using GitHubConnectorAPI.Models;

namespace GitHubConnectorAPI.Services
{
    public interface IGitHubService
    {
        Task<string> GetRepositories(string? owner = null);
        Task<string> ListIssues(string owner, string repo);
        Task<string> ListMilestones(string owner, string repo);
        Task<string> ListCommits(string owner, string repo);
        Task<string> CreateMilestone(string owner, string repo, CreateMilestoneRequest request);
        Task<string> CreateIssue(string owner, string repo, CreateIssueRequest request);
    }
}
