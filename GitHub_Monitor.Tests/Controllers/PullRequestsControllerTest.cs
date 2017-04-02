using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using GitHub_Monitor.Controllers;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services;
using GitHub_Monitor.Tests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace GitHub_Monitor.Tests.Controllers
{
	[TestFixture]
	public class PullRequestsControllerTest
	{
		#region Dependencies
		private PullRequestsController pullRequestsController;
		private Mock<IPullRequestService> mockPullRequestService;

		[SetUp]
		public void SetUp()
		{
			mockPullRequestService = new Mock<IPullRequestService>();
			pullRequestsController = new PullRequestsController() { PullRequestService = mockPullRequestService.Object };
		}
		#endregion


		#region Get(string, string)
		[Test]
		public async Task Get_ShouldReturnPullRequests_WhenThereArePullRequests()
		{
			// Setup
			var owner = "ampersandre";
			var name = "greyscale-color-palette";
			var expectedPullRequests = Enumerable.Range(1, 7).Select(Generator.GeneratePullRequest);
			mockPullRequestService.Setup(s => s.GetAll(owner, name)).Returns(() => Task.FromResult(expectedPullRequests));

			// Execute
			var actualPullRequests = await pullRequestsController.Get(owner, name);

			// Assert
			Assert.AreEqual(expectedPullRequests, actualPullRequests);
		}


		[Test]
		public async Task Get_ShouldReturnEmptyList_WhenThereAreNoPullRequests()
		{
			// Setup
			var owner = "microsoft";
			var name = "clippy-helper";
			IEnumerable<PullRequest> expectedPullRequests = new List<PullRequest>();
			mockPullRequestService.Setup(s => s.GetAll(owner, name)).Returns(() => Task.FromResult(expectedPullRequests));

			// Execute
			var actualPullRequests = await pullRequestsController.Get(owner, name);

			// Assert
			Assert.AreEqual(0, actualPullRequests.Count());
		}


		[Test]
		public async Task Get_ShouldThrow500_WhenPullRequestServiceThrowsError()
		{
			// Setup
			var owner = "ampersandre";
			var name = "a-wonderful-idea";
			mockPullRequestService.Setup(s => s.GetAll(owner, name)).Throws(new ArgumentException("Invalid Configuration"));

			// Execute
			try
			{
				await pullRequestsController.Get(owner, name);
				Assert.Fail("Expected HTTP 500 to be thrown");
			}
			catch (HttpResponseException exception)
			{
				// Assert
				Assert.AreEqual(HttpStatusCode.InternalServerError, exception.Response.StatusCode);
			}
		}
		#endregion


		#region Get(string, string, int)
		[Test]
		public async Task GetOne_ShouldReturnExpectedPullRequest_WhenTargetPullRequestExists()
		{
			// Setup
			var owner = "ampersandre";
			var name = "frog-translator";
			var id = 59;
			var expectedPullRequest = Generator.GeneratePullRequest(id);
			mockPullRequestService.Setup(s => s.GetOne(owner, name, id)).Returns(() => Task.FromResult(expectedPullRequest));

			// Execute
			var actualPullRequest = await pullRequestsController.Get(owner, name, id);

			// Assert
			Assert.AreEqual(expectedPullRequest, actualPullRequest);
		}


		[Test]
		public async Task GetOne_ShouldReturnNull_WhenPullRequestNotFound()
		{
			// Setup
			var owner = "ampersandre";
			var name = "library-delivery-service";
			var id = 12;
			mockPullRequestService.Setup(s => s.GetOne(owner, name, id)).Returns(() => Task.FromResult<PullRequest>(null));

			// Execute
			var actualPullRequest = await pullRequestsController.Get(owner, name, id);

			// Assert
			Assert.IsNull(actualPullRequest, "Should not have returned a pullRequest");
		}


		[Test]
		public async Task GetOne_ShouldThrow500_WhenPullRequestServiceThrowsError()
		{
			// Setup
			mockPullRequestService.Setup(s => s.GetOne(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Throws(new ArgumentException("Invalid Configuration"));

			// Execute
			try
			{
				await pullRequestsController.Get("foo", "bar", 14);
				Assert.Fail("Expected HTTP 500 to be thrown");
			}
			catch (HttpResponseException exception)
			{
				// Assert
				Assert.AreEqual(HttpStatusCode.InternalServerError, exception.Response.StatusCode);
			}
		}
		#endregion

	}
}
