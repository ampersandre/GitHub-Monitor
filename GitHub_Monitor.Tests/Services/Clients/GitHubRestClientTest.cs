using System.Net;
using System.Threading.Tasks;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services.Clients;
using GitHub_Monitor.Tests.TestUtilities;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace GitHub_Monitor.Tests.Services.Clients
{
	[TestFixture]
	public class GitHubRestClientTest
	{
		#region Dependencies
		private Mock<IRestClient> mockRestClient;
		private GitHubRestClient client;


		[SetUp]
		public void SetUp()
		{
			mockRestClient = new Mock<IRestClient>();
			client = new GitHubRestClient() { RestClient = mockRestClient.Object };
		}
		
		#endregion


		#region Get(string)
		[Test]
		public async Task Get_ShouldReturnExpectedObject_GivenResponse()
		{
			// Setup
			var expectedUser = Generator.GeneraterUser();
			var mockRestResponse = new Mock<IRestResponse<User>>();
			mockRestResponse.Setup(r => r.Data).Returns(() => expectedUser);
			mockRestClient.Setup(c => c.Execute<User>(It.IsAny<IRestRequest>())).Returns(() => mockRestResponse.Object);

			// Execute
			var actualUser = await client.Get<User>("users/ampersandre");

			// Assert
			Assert.IsNotNull(actualUser);
			AssertUtils.AreIdentical(expectedUser, actualUser);
		}
		#endregion
	}
}
