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

		private static void RegisterServices(IUnityContainer container)
		{
			container.RegisterType<IGitHubClient, GitHubRestClient>();
			container.RegisterType<IRepositoryService, RepositoryService>();
			container.RegisterType<IPullRequestService, PullRequestService>();
		}

		private static void RegisterCache(IUnityContainer container)
		{
			var redisConnectionString = ConfigurationManager.ConnectionStrings["redis"];
			var memcachedConnectionString = ConfigurationManager.ConnectionStrings["memcached"];
			if (redisConnectionString != null)
			{
				string configString = redisConnectionString.ConnectionString;
				var options = ConfigurationOptions.Parse(configString);
				var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(options);

				container.RegisterInstance(redisConnectionMultiplexer);
				container.RegisterType<ICacheClient, RedisCacheClient>();
			}
			else if (memcachedConnectionString != null)
			{
				container.RegisterType<ICacheClient, MemcachedCacheClient>();
			}
			else
			{
				container.RegisterType<ICacheClient, DoNothingCacheClient>();
			}
		}
	}
}