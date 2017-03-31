using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub_Monitor.Models;

namespace GitHub_Monitor.Services
{
	public interface IPullRequestService
	{
		Task<IEnumerable<PullRequest>> GetByRepositoryId();

		Task<PullRequest> GetById(int id);
	}
}
