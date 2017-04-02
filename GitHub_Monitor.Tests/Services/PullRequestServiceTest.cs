using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services;
using GitHub_Monitor.Services.Clients;
using GitHub_Monitor.Tests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace GitHub_Monitor.Tests.Services
{
	class PullRequestServiceTest
	{
		
		#region Dependencies

		private PullRequestService service;
		private Mock<IGitHubClient> mockGitHubClient;

		[SetUp]
		public void SetUp()
		{
			mockGitHubClient = new Mock<IGitHubClient>();

			service = new PullRequestService() { GitHubClient = mockGitHubClient.Object };
		}
		#endregion

		#region GetAll
		[Test]
		public async Task GetAll_ShouldReturnPullRequests_WhenThereAreSome()
		{
			// Setup
			var expectedPullRequests = Enumerable.Range(1, 4).Select(Generator.GeneratePullRequest).ToList();
			mockGitHubClient.Setup(c => c.Get<List<PullRequest>>("repos/ampersandre/barn-ville/pulls")).Returns(Task.FromResult(expectedPullRequests));

			// Execute
			var actualPullRequests = await service.GetAll("ampersandre", "barn-ville");

			// Assert
			AssertUtils.AreIdentical(expectedPullRequests, actualPullRequests);
		}

		public async Task GetAll_ShouldReturnEmptySet_WhenThereAreNone()
		{
			// Setup
			var emptyList = new List<PullRequest>();
			mockGitHubClient.Setup(c => c.Get<List<PullRequest>>("repos/ampersandre/random-scale/pulls")).Returns(Task.FromResult(emptyList));

			// Execute
			var actualPullRequests = await service.GetAll("ampersandre", "random-scale");

			// Assert
			AssertUtils.AreIdentical(emptyList, actualPullRequests);
		}
		#endregion

		#region GetOne
		[Test]
		public async Task GetOne_ShouldReturnExpectedPullRequest_WhenItExists()
		{
			// Setup
			var owner = "ampersandre";
			var name = "fitbit-except-for-blinking";
			var expectedPullRequest = Generator.GeneratePullRequest();
			mockGitHubClient.Setup(c => c.Get<PullRequest>($"repos/{owner}/{name}/pulls/78")).Returns(Task.FromResult(expectedPullRequest));

			// Execute
			var actualPullRequest = await service.GetOne(owner, name, 78);

			// Assert
			AssertUtils.AreIdentical(expectedPullRequest, actualPullRequest);
		}

		[Test]
		public async Task GetOne_ShouldReturnNull_WhenItDoesNotExist()
		{
			// Setup
			var owner = "ampersandre";
			var name = "slack-over-sms";
			mockGitHubClient.Setup(c => c.Get<PullRequest>($"repos/{owner}/{name}/pulls/25")).Returns(Task.FromResult<PullRequest>(null));

			// Execute
			var actualPullRequest = await service.GetOne(owner, name, 25);

			// Assert
			Assert.IsNull(actualPullRequest);
		}
		#endregion



	}
}
