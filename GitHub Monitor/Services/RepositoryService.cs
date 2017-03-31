using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services.Clients;
using Microsoft.Practices.Unity;

namespace GitHub_Monitor.Services
{
	public class RepositoryService : IRepositoryService
	{
		[Dependency]
		public IGitHubClient GitHubClient { get; set; }

		public async Task<IEnumerable<Repository>> GetAll()
		{
			var repositories = await GitHubClient.Get<List<Repository>>("user/repos");

			return repositories;
		}

		public async Task<Repository> GetOne(string owner, string name)
		{
			var repository = await GitHubClient.Get<Repository>($"repos/${owner}/${name}");
			return repository;
		}
	}
}