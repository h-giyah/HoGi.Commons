using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace HoGi.Commons.Interfaces.Caches
{
    public interface ICacheService
    {
        Task<T> AddAsync<T>(string key, Func<Task<T>> addItemFactory);
        Task<T> AddAsync<T>(string key, Func<Task<T>> addItemFactory, DistributedCacheEntryOptions options);
        Task<T> AddAsync<T>(string key, T addItem);
        Task<T> AddAsync<T>(string key, T addItem, DistributedCacheEntryOptions options);
        T Add<T>(string key, Func<T> addItemFactory);
        T Add<T>(string key, Func<T> addItemFactory, DistributedCacheEntryOptions options);
        T Add<T>(string key, T addItem);
        T Add<T>(string key, T addItem, DistributedCacheEntryOptions options);
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> addItemFactory);
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> addItemFactory, DistributedCacheEntryOptions options);
        T GetOrAdd<T>(string key, Func<T> addItemFactory);
        T GetOrAdd<T>(string key, Func<T> addItemFactory, DistributedCacheEntryOptions options);
        Task<T> GetAsync<T>(string key);
        T Get<T>(string key);
        Task Remove(string key);
    }
}