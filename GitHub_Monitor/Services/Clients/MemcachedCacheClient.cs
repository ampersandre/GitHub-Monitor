using System;
using System.Threading.Tasks;

namespace GitHub_Monitor.Services.Clients
{
	public class MemcachedCacheClient : ICacheClient
	{
		public T Get<T>(string key)
		{
			throw new NotImplementedException();
		}

		public Task<T> Get<T>(string key, Func<Task<T>> resolver)
		{
			throw new NotImplementedException();
		}

		public Task Set<T>(string key, T value)
		{
			throw new NotImplementedException();
		}
	}
}