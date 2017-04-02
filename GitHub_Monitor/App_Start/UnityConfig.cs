using System;
using Microsoft.Practices.Unity;
using GitHub_Monitor.Services;
using GitHub_Monitor.Services.Clients;
using System.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using StackExchange.Redis;

namespace GitHub_Monitor.App_Start
{
	/// <summary>
	/// Specifies the Unity configuration for the main container.
	/// </summary>
	public class UnityConfig
	{
		#region Unity Container
		private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
		{
			var container = new UnityContainer();
			RegisterTypes(container);
			return container;
		});

		/// <summary>
		/// Gets the configured Unity container.
		/// </summary>
		public static IUnityContainer GetConfiguredContainer()
		{
			return container.Value;
		}
		#endregion

		public static void RegisterTypes(IUnityContainer container)
		{
			RegisterRestClient(container);
			RegisterCache(container);
			RegisterServices(container);
		}

		private static void RegisterRestClient(IUnityContainer container)
		{
			var restClient = new RestClient("https://api.github.com");

			var accessToken = Environment.GetEnvironmentVariable("GITHUB_ACCESS_TOKEN")
							?? ConfigurationManager.AppSettings["gitHubAccessToken"];
			if (String.IsNullOrWhiteSpace(accessToken))
			{
				throw new ArgumentException("Missing GitHub Access Token. Please set the GITHUB_ACCESS_TOKEN environment variable or provide the gitHubAccessToken AppSetting");
			}
			restClient.Authenticator = new OAuth2UriQueryParameterAuthenticator(accessToken);

			container.RegisterInstance<IRestClient>(restClient);
		}

		private static void RegisterServices(IUnityContainer container)
		{
			container.RegisterType<IGitHubClient, GitHubRestClient>();
			container.RegisterType<IRepositoryService, RepositoryService>();
			container.RegisterType<IPullRequestService, PullRequestService>();
		}


		private static void RegisterCache(IUnityContainer container)
		{
			var redisConnectionString = ConfigurationManager.ConnectionStrings["redis"];
			if (redisConnectionString != null)
			{
				string configString = redisConnectionString.ConnectionString;
				var options = ConfigurationOptions.Parse(configString);
				var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(options);

				container.RegisterInstance(redisConnectionMultiplexer);
				container.RegisterType<ICacheClient, RedisCacheClient>();
			}
			else
			{
				// Try to initialize MemcachedCacheClient
			}

		}
	}
}
