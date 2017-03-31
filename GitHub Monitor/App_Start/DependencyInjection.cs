using System.Configuration;
using System.Web.Mvc;
using GitHub_Monitor.Services;
using GitHub_Monitor.Services.Clients;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using StackExchange.Redis;

namespace GitHub_Monitor.App_Start
{
	public class DependencyInjection
	{
		public static void RegisterDependencies()
		{
			var container = new UnityContainer();

			RegisterCache(container);
			RegisterServices(container);
			
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}

		private static void RegisterServices(UnityContainer container)
		{
			container.RegisterType<IGitHubClient, GitHubRestClient>();
			container.RegisterType<IRepositoryService, RepositoryService>();
			container.RegisterType<IPullRequestService, PullRequestService>();
		}

		private static void RegisterCache(UnityContainer container)
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