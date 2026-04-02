namespace GitHubConnectorAPI.Services
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public string Content { get; }

        public ApiException(int statusCode, string content)
            : base($"GitHub API returned {statusCode}: {content}")
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
