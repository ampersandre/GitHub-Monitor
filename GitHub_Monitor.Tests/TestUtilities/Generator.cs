using System;
using System.Web.Configuration;
using GitHub_Monitor.Models;

namespace GitHub_Monitor.Tests.TestUtilities
{
	public class Generator
	{
		private static readonly Random random = new Random();

		#region Repositories
		public static Repository GenerateRepository(int id = 0)
		{
			return new Repository()
			{
				Id = id > 0 ? id : random.Next(),
				Name = Guid.NewGuid().ToString().Substring(0, 8),
				Owner = GeneraterUser(),
				HtmlUrl = Guid.NewGuid().ToString().Substring(0, 20),
				ForksCount = random.Next(),
				OpenIssuesCount = random.Next(),
				StarGazersCount = random.Next(),
				WatchersCount = random.Next()
			};
		}
		#endregion

		#region PullRequests
		public static PullRequest GeneratePullRequest(int id = 0)
		{
			return new PullRequest()
			{
				Id = id > 0 ? id : random.Next(),
				HtmlUrl = Guid.NewGuid().ToString().Substring(0, 20)
			};
		}
		#endregion


		#region Owner
		public static User GeneraterUser(int id = 0)
		{
			return new User()
			{
				Id = id > 0 ? id : random.Next(),
				Login = Guid.NewGuid().ToString().Substring(0, 8),
				AvatarUrl = Guid.NewGuid().ToString(),
				HtmlUrl = Guid.NewGuid().ToString().Substring(0, 20)
			};
		}
		#endregion
	}
}
