using System;
using System.Threading.Tasks;

namespace GitHub_Monitor.Services.Clients
{
	public interface ICacheClient
	{
		/// <summary>
		/// Attempt to fetch the object from cache
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns>The object, or null</returns>
		T Get<T>(string key);

		/// <summary>
		/// Attempt to object the value from cache.
		/// If it isn't there, store and return whatever <see cref="resolver"/> returns
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="resolver"></param>
		/// <returns>The object, and stores it in the cache</returns>
		Task<T> Get<T>(string key, Func<Task<T>> resolver);

		/// <summary>
		/// Store <see cref="value"/> in the cache under <see cref="key"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		Task Set<T>(string key, T value);
	}
}
