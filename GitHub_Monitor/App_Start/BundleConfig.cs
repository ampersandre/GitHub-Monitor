using System.Web.Optimization;

namespace GitHub_Monitor
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			#region Scripts
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/ui").Include(
						"~/Scripts/handlebars.min.js",
						"~/Scripts/bootstrap.js",
						"~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/util").Include(
						"~/Scripts/app/api.js"));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));
			#endregion

			#region Styles
			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/elements/spinkit.css",
					  "~/Content/site.css"));
			#endregion
		}
	}
}
