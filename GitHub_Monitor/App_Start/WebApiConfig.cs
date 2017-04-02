using System.Net.Http.Headers;
using System.Web.Http;

namespace GitHub_Monitor
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Force JsonFormatter to respond to text/html requests
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "RepositoriesApi",
				routeTemplate: "api/repositories/{owner}/{name}",
				defaults: new { controller = "Repositories" }
			);

			config.Routes.MapHttpRoute(
				name: "PullRequestsApi",
				routeTemplate: "api/pullrequests/{owner}/{name}/{id}",
				defaults: new { controller = "PullRequests", id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
