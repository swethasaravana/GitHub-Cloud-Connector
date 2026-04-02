using GitHubConnectorAPI.Models;
using GitHubConnectorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitHubConnector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        //Returns public repositories for that user.
        [HttpGet("repos")]
        public async Task<IActionResult> GetRepos([FromQuery] string? owner)
        {
            try
            {
                var json = await _gitHubService.GetRepositories(owner);
                return Content(json, "application/json");
            }
            catch (System.InvalidOperationException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Content });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //Requires GITHUB_PAT environment variable to be set for authentication.
        [HttpPost("repos/{owner}/{repo}/create-issues")]
        public async Task<IActionResult> CreateIssue(string owner, string repo, [FromBody] CreateIssueRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new { error = "Request must include a title." });
            }

            try
            {
                var json = await _gitHubService.CreateIssue(owner, repo, request);
                return Content(json, "application/json");
            }
            catch (System.InvalidOperationException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Content });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //Returns issues for the given repository (public or private depending on token)
        [HttpGet("repos/{owner}/{repo}/list-issues")]
        public async Task<IActionResult> ListIssues(string owner, string repo)
        {
            try
            {
                var json = await _gitHubService.ListIssues(owner, repo);
                return Content(json, "application/json");
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Content });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //Requires appropriate PAT scopes (repo/public_repo)
        [HttpPost("repos/{owner}/{repo}/create-milestones")]
        public async Task<IActionResult> CreateMilestone(string owner, string repo, [FromBody] CreateMilestoneRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new { error = "Request must include a title." });
            }

            try
            {
                var json = await _gitHubService.CreateMilestone(owner, repo, request);
                return Content(json, "application/json");
            }
            catch (GitHubConnectorAPI.Services.ApiException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Content });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //Returns milestones for the given repository.
        [HttpGet("repos/{owner}/{repo}/list-milestones")]
        public async Task<IActionResult> ListMilestones(string owner, string repo)
        {
            try
            {
                var json = await _gitHubService.ListMilestones(owner, repo);
                return Content(json, "application/json");
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Content });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //Returns commits for the given repository.
        [HttpGet("repos/{owner}/{repo}/list-commits")]
        public async Task<IActionResult> ListCommits(string owner, string repo)
        {
            try
            {
                var json = await _gitHubService.ListCommits(owner, repo);
                return Content(json, "application/json");
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Content });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}