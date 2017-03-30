using System.Web.Mvc;
using GitHub_Monitor.Services;
using GitHub_Monitor.Services.Clients;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace GitHub_Monitor.App_Start
{
	public class DependencyInjection
	{
		public static void RegisterDependencies()
		{
			var container = new UnityContainer();

			container.RegisterType<IGitHubClient, GitHubRestClient>();
			container.RegisterType<IRepositoryService, RepositoryService>();
			container.RegisterType<IPullRequestService, PullRequestService>();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}