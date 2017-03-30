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
			var expectedRepositories = Enumerable.Range(1, 7).Select(Generator.GenerateRepository).ToList();
			mockRepositoryService.Setup(s => s.Get()).Returns(() => Task.FromResult(expectedRepositories));

			// Execute
			var actualRepositories = await repositoriesController.Get();

			// Assert
			Assert.AreEqual(expectedRepositories, actualRepositories);
		}


		[Test]
		public async Task Get_ShouldReturnEmptyList_WhenThereAreNoRepositories()
		{
			// Setup
			var expectedRepositories = new List<Repository>();
			mockRepositoryService.Setup(s => s.Get()).Returns(() => Task.FromResult(expectedRepositories));

			// Execute
			var actualRepositories = await repositoriesController.Get();

			// Assert
			Assert.AreEqual(0, actualRepositories.Count());
		}


		[Test]
		public async Task Get_ShouldThrow500_WhenRepositoryServiceThrowsError()
		{
			// Setup
			mockRepositoryService.Setup(s => s.Get()).Throws(new ArgumentException("Invalid Configuration"));

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


		#region GetById
		[Test]
		public async Task GetById_ShouldReturnExpectedRepository_WhenTargetRepositoryExists()
		{
			// Setup
			var repositories = Enumerable.Range(1, 7).Select(Generator.GenerateRepository).ToList();
			mockRepositoryService.Setup(s => s.Get()).Returns(() => Task.FromResult(repositories));
			var expectedRepository = repositories.Single(r => r.Id == 5);

			// Execute
			var actualRepository = await repositoriesController.Get(5);

			// Assert
			Assert.AreEqual(expectedRepository, actualRepository);
		}


		[Test]
		public async Task GetById_ShouldReturnNull_WhenTargetRepositoryDoesNotExist()
		{
			// Setup
			var missingRepositoryId = 5;
			var expectedRepositories = Enumerable.Range(1, 7)
												.Select(Generator.GenerateRepository)
												.Where(r => r.Id != missingRepositoryId) // Remove ID = 5
												.ToList();
			mockRepositoryService.Setup(s => s.Get()).Returns(() => Task.FromResult(expectedRepositories));

			// Execute
			var actualRepository = await repositoriesController.Get(missingRepositoryId);

			// Assert
			Assert.IsNull(actualRepository, "Should not have returned a repository");
		}


		[Test]
		public async Task GetById_ShouldReturnNull_WhenThereAreNoRepositoriesAtAll()
		{
			// Setup
			var expectedRepositories = new List<Repository>();
			mockRepositoryService.Setup(s => s.Get()).Returns(() => Task.FromResult(expectedRepositories));

			// Execute
			var actualRepository = await repositoriesController.Get(5);

			// Assert
			Assert.IsNull(actualRepository, "Should not have returned a repository");
		}


		[Test]
		public async Task RepositoriesController_ShouldThrow500_WhenRepositoryServiceThrowsError()
		{
			// Setup
			mockRepositoryService.Setup(s => s.Get()).Throws(new ArgumentException("Invalid Configuration"));

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


	}
}
