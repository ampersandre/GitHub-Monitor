using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services;
using GitHub_Monitor.Services.Clients;
using GitHub_Monitor.Tests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace GitHub_Monitor.Tests.Services
{
	[TestFixture]
	public class RepositoryServiceTest
	{
		#region Dependencies

		private RepositoryService service;
		private Mock<IGitHubClient> mockGitHubClient;

		[SetUp]
		public void SetUp()
		{
			mockGitHubClient = new Mock<IGitHubClient>();

			service = new RepositoryService() { GitHubClient = mockGitHubClient.Object };
		}
		#endregion

		#region GetAll
		[Test]
		public async Task GetAll_ShouldReturnRepositories_WhenThereAreSome()
		{
			// Setup
			var expectedRepositories = Enumerable.Range(1, 4).Select(Generator.GenerateRepository).ToList();
			mockGitHubClient.Setup(c => c.Get<List<Repository>>("user/repos")).Returns(Task.FromResult(expectedRepositories));

			// Execute
			var actualRepositories = await service.GetAll();

			// Assert
			AssertUtils.AreIdentical(expectedRepositories, actualRepositories);
		}

		public async Task GetAll_ShouldReturnEmptySet_WhenThereAreNone()
		{
			// Setup
			var emptyList = new List<Repository>();
			mockGitHubClient.Setup(c => c.Get<List<Repository>>("user/repos")).Returns(Task.FromResult(emptyList));

			// Execute
			var actualRepositories = await service.GetAll();

			// Assert
			AssertUtils.AreIdentical(emptyList, actualRepositories);
		}
		#endregion

		#region GetOne
		[Test]
		public async Task GetOne_ShouldReturnExpectedRepository_WhenItExists()
		{
			// Setup
			var owner = "ampersandre";
			var name = "fitbit-except-for-blinking";
			var expectedRepository = Generator.GenerateRepository();
			mockGitHubClient.Setup(c => c.Get<Repository>($"repos/${owner}/${name}")).Returns(Task.FromResult(expectedRepository));

			// Execute
			var actualRepository = await service.GetOne(owner, name);

			// Assert
			AssertUtils.AreIdentical(expectedRepository, actualRepository);
		}

		[Test]
		public async Task GetOne_ShouldReturnNull_WhenItDoesNotExist()
		{
			// Setup
			var owner = "ampersandre";
			var name = "slack-over-sms";
			mockGitHubClient.Setup(c => c.Get<Repository>($"repos/${owner}/${name}")).Returns(Task.FromResult<Repository>(null));

			// Execute
			var actualRepository = await service.GetOne(owner, name);

			// Assert
			Assert.IsNull(actualRepository);
		}
		#endregion


	}
}
