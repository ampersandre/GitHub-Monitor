using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using RestSharp;

namespace GitHub_Monitor.Services.Clients
{
	public class GitHubRestClient : IGitHubClient
	{
		[Dependency]
		public IRestClient RestClient { get; set; }


		public Task<T> Get<T>(string url) where T : new()
		{
			return Task.Run(() =>
			{
				var request = new RestRequest(url, Method.GET);
					
				return RestClient.Execute<T>(request).Data;
			});
		}
	}
}