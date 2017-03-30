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
				var repositories = await RepositoryService.Get();
				return repositories;
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}

		// GET: /repositories/id
		public async Task<Repository> Get(int id)
		{
			try
			{
				// TODO: Implement GetById in RepositoryService
				var repositories = await RepositoryService.Get();
				return repositories.SingleOrDefault(repo => repo.Id == id);
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}
	}
}