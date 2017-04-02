using System;
using System.Threading.Tasks;

namespace GitHub_Monitor.Services.Clients
{
	public class DoNothingCacheClient : ICacheClient
	{
		public T Get<T>(string key)
		{
			return default(T);
		}

		public async Task<T> Get<T>(string key, Func<Task<T>> resolver)
		{
			return await resolver();
		}

		public Task Set<T>(string key, T value)
		{
			return Task.Run(() => { });
		}
	}
}