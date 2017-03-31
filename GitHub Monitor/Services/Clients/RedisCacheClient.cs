using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace GitHub_Monitor.Services.Clients
{
	public class RedisCacheClient : ICacheClient
	{
		#region Dependencies
		[Dependency]
		public IConnectionMultiplexer RedisConnection { get; set; }
		#endregion

		// Get the value directly
		public T Get<T>(string key)
		{
			Trace.Assert(!string.IsNullOrWhiteSpace(key));
			var prefixedKey = PrefixKey(key);

			try
			{
				var json = Db().StringGet(prefixedKey, CommandFlags.None);
				var value = JsonConvert.DeserializeObject<T>(json);
				return value;
			}
			catch
			{
				return default(T);
			}
		}

		// Get from cache, or fetch the value and put it in the cache
		public async Task<T> Get<T>(string key, Func<Task<T>> resolver)
		{
			var value = Get<T>(key);
			if (value == null)
			{
				value = await resolver();

				await Set(key, value);
			}

			return value;
		}

		// Return immediately rather than awaiting in case the caller doesn't care
		public Task Set<T>(string key, T value)
		{
			Trace.Assert(!string.IsNullOrWhiteSpace(key));

			var prefixedKey = PrefixKey(key);
			var json = JsonConvert.SerializeObject(value);
			return Db().StringSetAsync(prefixedKey, json, TimeSpan.FromMinutes(3), When.Always, CommandFlags.None);
		}


		#region Utilities
		private string PrefixKey(string str)
		{
			return $"github-monitor:${str}";
		}

		private IDatabase Db()
		{
			return RedisConnection.GetDatabase(-1, null);
		}
		#endregion
	}
}