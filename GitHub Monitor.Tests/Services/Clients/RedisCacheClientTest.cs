using System;
using System.Threading.Tasks;
using GitHub_Monitor.Models;
using GitHub_Monitor.Services.Clients;
using GitHub_Monitor.Tests.TestUtilities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using StackExchange.Redis;

namespace GitHub_Monitor.Tests.Services.Clients
{
	[TestFixture]
	public class RedisCacheClientTest
	{
		#region Dependencies
		private RedisCacheClient redisCacheClient;
		private Mock<IConnectionMultiplexer> mockRedisConnection;
		private Mock<IDatabase> mockRedisDb;

		[SetUp]
		public void SetUp()
		{
			mockRedisConnection = new Mock<IConnectionMultiplexer>();
			mockRedisDb = new Mock<IDatabase>();
			mockRedisConnection.Setup(c => c.GetDatabase(-1, null)).Returns(mockRedisDb.Object);

			redisCacheClient = new RedisCacheClient() { RedisConnection = mockRedisConnection.Object };
		}
		#endregion

		#region GetAll(string)
		[Test]
		public void Get_ShouldReturnExpectedValue_WhenCacheContainsValue()
		{
			// Setup
			var key = "repository:76";
			var expectedValue = Generator.GenerateRepository(76);
			var valueJson = JsonConvert.SerializeObject(expectedValue);
			mockRedisDb.Setup(db => db.StringGet($"github-monitor:${key}", CommandFlags.None)).Returns(valueJson);

			// Execute
			var actualValue = redisCacheClient.Get<Repository>(key);

			// Assert
			AssertUtils.AreIdentical(expectedValue, actualValue);
		}

		[Test]
		public void Get_ShouldReturnNull_WhenCacheDoesNotContainValue()
		{
			// Setup
			var key = "repository:76";
			mockRedisDb.Setup(db => db.StringGet($"github-monitor:${key}", CommandFlags.None)).Returns(default(string));

			// Execute
			var actualValue = redisCacheClient.Get<Repository>(key);

			// Assert
			Assert.IsNull(actualValue);
		}

		[Test]
		public void Get_ShouldReturnNull_WhenCacheThrowsException()
		{
			// Setup
			var key = "repository:76";
			mockRedisDb.Setup(db => db.StringGet($"github-monitor:${key}", CommandFlags.None)).Throws(new Exception("Not enough $"));

			// Execute
			var actualValue = redisCacheClient.Get<Repository>(key);

			// Assert
			Assert.IsNull(actualValue);
		}
		#endregion

		#region GetAll(string, func)
		[Test]
		public async Task GetWithFunc_ShouldReturnExpectedValue_WhenCacheContainsValue()
		{
			// Setup
			var key = "repository:23";
			var expectedValue = Generator.GenerateRepository(23);
			expectedValue.Name = "The original!";
			var valueJson = JsonConvert.SerializeObject(expectedValue);
			mockRedisDb.Setup(db => db.StringGet($"github-monitor:${key}", CommandFlags.None)).Returns(valueJson);

			Func<Task<Repository>> resolver = () => {
				return Task.Run(() => Generator.GenerateRepository(23));
			};

			// Execute
			var actualValue = await redisCacheClient.Get(key, resolver);

			// Assert
			AssertUtils.AreIdentical(expectedValue, actualValue);
		}

		[Test]
		public async Task GetWithFunc_ShouldReturnExpectedValue_WhenCacheDoesNotContainValue()
		{
			// Setup
			var key = "repository:23";
			var expectedValue = Generator.GenerateRepository(23);
			var valueJson = JsonConvert.SerializeObject(expectedValue);
			mockRedisDb.Setup(db => db.StringGet($"github-monitor:${key}", CommandFlags.None)).Returns(default(string));

			Func<Task<Repository>> resolver = () => {
				return Task.Run(() => expectedValue);
			};

			// Execute
			var actualValue = await redisCacheClient.Get(key, resolver);

			// Assert
			AssertUtils.AreIdentical(expectedValue, actualValue);
			mockRedisDb.Verify(db => db.StringSetAsync($"github-monitor:${key}", valueJson, TimeSpan.FromMinutes(3), When.Always, CommandFlags.None));
		}
		#endregion

	}
}
