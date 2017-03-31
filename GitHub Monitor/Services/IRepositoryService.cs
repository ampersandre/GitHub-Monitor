using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub_Monitor.Models;

namespace GitHub_Monitor.Services
{
	public interface IRepositoryService
	{
		Task<IEnumerable<Repository>> GetAll();

		Task<Repository> GetOne(string owner, string name);
	}
}
