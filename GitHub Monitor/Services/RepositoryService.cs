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
		public IGitHubClient client { get; set; }

		public async Task<List<Repository>> Get()
		{
			var repositories = await client.Get<List<Repository>>("user/repos");

			return repositories;
		}
	}
}