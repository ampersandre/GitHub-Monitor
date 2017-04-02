using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services;
using Microsoft.Practices.Unity;

namespace GitHub_Monitor.Controllers
{
	public class PullRequestsController : ApiController
	{
		[Dependency]
		public IPullRequestService PullRequestService { get; set; }

		// GET: /repositories/:owner/:name
		public async Task<IEnumerable<PullRequest>>  Get(string owner, string name)
		{
			try
			{
				var pullRequests = await PullRequestService.GetAll(owner, name);

				return pullRequests;
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}

		// GET: /repositories/:owner/:name/:id
		public async Task<PullRequest> Get(string owner, string name, int id)
		{
			try
			{
				var pullRequest = await PullRequestService.GetOne(owner, name, id);

				return pullRequest;
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
		}
	}
}