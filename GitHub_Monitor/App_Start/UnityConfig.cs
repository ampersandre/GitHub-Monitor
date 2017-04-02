using System;
using Microsoft.Practices.Unity;
using GitHub_Monitor.Services;
using GitHub_Monitor.Services.Clients;
using System.Configuration;
using RestSharp;
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
			RegisterCache(container);
			RegisterServices(container);
		}



		private static void RegisterServices(IUnityContainer container)
		{
			container.RegisterType<IRestClient, RestClient>();
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
