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
	public class RepositoriesControllerTest
	{
		#region Dependencies
		private RepositoriesController repositoriesController;
		private Mock<IRepositoryService> mockRepositoryService;

		[SetUp]
		public void SetUp()
		{
			mockRepositoryService = new Mock<IRepositoryService>();
			repositoriesController = new RepositoriesController() { RepositoryService = mockRepositoryService.Object };
		}
		#endregion


		#region Get
		[Test]
		public async Task Get_ShouldReturnRepositories_WhenThereAreRepositories()
		{
			// Setup
			var expectedRepositories = Enumerable.Range(1, 7).Select(Generator.GenerateRepository);
			mockRepositoryService.Setup(s => s.GetAll()).Returns(() => Task.FromResult(expectedRepositories));

			// Execute
			var actualRepositories = await repositoriesController.Get();

			// Assert
			Assert.AreEqual(expectedRepositories, actualRepositories);
		}


		[Test]
		public async Task Get_ShouldReturnEmptyList_WhenThereAreNoRepositories()
		{
			// Setup
			IEnumerable<Repository> expectedRepositories = new List<Repository>();
			mockRepositoryService.Setup(s => s.GetAll()).Returns(() => Task.FromResult(expectedRepositories));

			// Execute
			var actualRepositories = await repositoriesController.Get();

			// Assert
			Assert.AreEqual(0, actualRepositories.Count());
		}


		[Test]
		public async Task Get_ShouldThrow500_WhenRepositoryServiceThrowsError()
		{
			// Setup
			mockRepositoryService.Setup(s => s.GetAll()).Throws(new ArgumentException("Invalid Configuration"));

			// Execute
			try
			{
				await repositoriesController.Get();
				Assert.Fail("Expected HTTP 500 to be thrown");
			}
			catch (HttpResponseException exception)
			{
				// Assert
				Assert.AreEqual(HttpStatusCode.InternalServerError, exception.Response.StatusCode);
			}
		}
		#endregion


		#region Get
		[Test]
		public async Task GetOne_ShouldReturnExpectedRepository_WhenTargetRepositoryExists()
		{
			// Setup
			var owner = "ampersandre";
			var name = "frog-translator";
			var expectedRepository = Generator.GenerateRepository();
			mockRepositoryService.Setup(s => s.GetOne(owner, name)).Returns(() => Task.FromResult(expectedRepository));

			// Execute
			var actualRepository = await repositoriesController.Get(owner, name);

			// Assert
			Assert.AreEqual(expectedRepository, actualRepository);
		}
		

		[Test]
		public async Task GetOne_ShouldReturnNull_WhenRepositoryNotFound()
		{
			// Setup
			var owner = "ampersandre";
			var name = "library-delivery-service";
			mockRepositoryService.Setup(s => s.GetOne(owner, name)).Returns(() => Task.FromResult<Repository>(null));

			// Execute
			var actualRepository = await repositoriesController.Get(owner, name);

			// Assert
			Assert.IsNull(actualRepository, "Should not have returned a repository");
		}
		#endregion

	}
}
