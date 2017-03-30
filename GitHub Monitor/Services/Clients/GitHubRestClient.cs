using System;
using System.Configuration;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace GitHub_Monitor.Services.Clients
{
	public class GitHubRestClient : IGitHubClient
	{
		private readonly RestClient client;

		public GitHubRestClient()
		{
			client = new RestClient("https://api.github.com");
			
			var accessToken = Environment.GetEnvironmentVariable("GITHUB_ACCESS_TOKEN")
							?? ConfigurationManager.AppSettings["gitHubAccessToken"];
			if (String.IsNullOrWhiteSpace(accessToken))
			{
				throw new ArgumentException("Missing GitHub Access Token. Please set the GITHUB_ACCESS_TOKEN environment variable or provide the gitHubAccessToken AppSetting");
			}
			client.Authenticator = new OAuth2UriQueryParameterAuthenticator(accessToken);
		}


		public Task<T> Get<T>(string url) where T : new()
		{
			return Task.Run(() =>
			{
				var request = new RestRequest(url, Method.GET);

				return client.Execute<T>(request).Data;
			});
		}
	}
}