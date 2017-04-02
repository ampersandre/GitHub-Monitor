using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub_Monitor.Models;

namespace GitHub_Monitor.Services
{
	public interface IPullRequestService
	{
		Task<IEnumerable<PullRequest>> GetAll(string owner, string name);

		Task<PullRequest> GetOne(string owner, string name, int id);
	}
}
