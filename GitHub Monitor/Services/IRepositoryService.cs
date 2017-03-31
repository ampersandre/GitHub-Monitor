using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub_Monitor.Models;

namespace GitHub_Monitor.Services
{
	public interface IRepositoryService
	{
		Task<List<Repository>> Get();

		Task<Repository> GetById();
	}
}
