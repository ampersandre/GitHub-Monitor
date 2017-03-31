using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services;
using Microsoft.Practices.Unity;

namespace GitHub_Monitor.Controllers
{
	public class RepositoriesController : ApiController
	{
		[Dependency]
		public IRepositoryService RepositoryService { get; set; }

		// GET: /repositories
		public async Task<IEnumerable<Repository>> Get()
		{
			try
			{
				var repositories = await RepositoryService.GetAll();
				return repositories;
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}

		// GET: /repositories/:owner/:name
		public async Task<Repository> Get(string owner, string name)
		{
			try
			{
				var repository = await RepositoryService.GetOne(owner, name);
				return repository;
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}
	}
}