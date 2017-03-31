using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub_Monitor.Models;

namespace GitHub_Monitor.Services
{
	public class PullRequestService : IPullRequestService
	{
		public Task<IEnumerable<PullRequest>> GetByRepositoryId()
		{
			throw new System.NotImplementedException();
		}

		public Task<PullRequest> GetById(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}