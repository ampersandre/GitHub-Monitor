using System.Threading.Tasks;
using RestSharp;

namespace GitHub_Monitor.Services.Clients
{
	public interface IGitHubClient
	{
		Task<T> Get<T>(string url) where T : new();
	}
}
