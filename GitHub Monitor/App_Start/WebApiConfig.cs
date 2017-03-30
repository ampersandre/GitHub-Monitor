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
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
