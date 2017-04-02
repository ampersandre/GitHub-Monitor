using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services.Clients;
using Microsoft.Practices.Unity;

namespace GitHub_Monitor.Services
{
	public class PullRequestService : IPullRequestService
	{
		[Dependency]
		public IGitHubClient GitHubClient { get; set; }

		public async Task<IEnumerable<PullRequest>> GetAll(string owner, string name)
		{
			var pullRequests = await GitHubClient.Get<List<PullRequest>>($"repos/{owner}/{name}/pulls");

			return pullRequests;
		}

		public async Task<PullRequest> GetOne(string owner, string name, int id)
		{
			var pullRequest = await GitHubClient.Get<PullRequest>($"repos/{owner}/{name}/pulls/{id}");
			return pullRequest;
		}
	}
}