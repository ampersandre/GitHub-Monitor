using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GitHub_Monitor.Services;
using Microsoft.Practices.Unity;

namespace GitHub_Monitor.Controllers
{
	public class IndexController : Controller
	{
		[Dependency]
		public IRepositoryService RepositoryService { get; set; }

		public async Task<ActionResult> Index()
		{
			try
			{
				var repositories = await RepositoryService.Get();
				return View(repositories);
			}
			catch (Exception)
			{
				ViewBag.Message = "Server Error, please try again later";
				return View();
			}
		}
	}
}