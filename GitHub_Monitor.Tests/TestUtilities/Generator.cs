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
				Owner = GenerateOwner(),
				HtmlUrl = Guid.NewGuid().ToString().Substring(0, 20),
				ForksCount = random.Next(),
				OpenIssuesCount = random.Next(),
				StarGazersCount = random.Next(),
				WatchersCount = random.Next()
			};
		}
		#endregion


		#region Owner
		public static Owner GenerateOwner(int id = 0)
		{
			return new Owner()
			{
				Id = id > 0 ? id : random.Next(),
				Login = Guid.NewGuid().ToString().Substring(0, 8),
				HtmlUrl = Guid.NewGuid().ToString().Substring(0, 20)
			};
		}
		#endregion
	}
}
