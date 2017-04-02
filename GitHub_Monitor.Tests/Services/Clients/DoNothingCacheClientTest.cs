using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHub_Monitor.Services.Clients;
using NUnit.Framework;

namespace GitHub_Monitor.Tests.Services.Clients
{
	[TestFixture]
	public class DoNothingCacheClientTest
	{
		#region Dependencies
		private DoNothingCacheClient cacheClient;

		[SetUp]
		public void SetUp()
		{
			cacheClient = new DoNothingCacheClient();
		}
		#endregion

		#region Get(string)

		[Test]
		public void Get_ShouldReturnNull()
		{
			// Execute
			var actualValue = cacheClient.Get<string>("anything");

			// Assert
			Assert.IsNull(actualValue);
		}
		#endregion

		#region Get(string, func)
		public void Get_ShouldReturnResolverValue()
		{
			// Setup
			var expectedValue = "hello";
			Func<Task<string>> resolver = () => Task.Run(() => "hello");

			// Execute
			var actualValue = cacheClient.Get("anything", resolver);

			// Assert
			Assert.AreEqual(expectedValue, actualValue);
		}
		#endregion

		#region Set(string, value)
		public async void Set_ShouldPass()
		{
			// Execute
			await cacheClient.Set("anything", "hello");

			Assert.Pass();
		}
		#endregion
	}
}
