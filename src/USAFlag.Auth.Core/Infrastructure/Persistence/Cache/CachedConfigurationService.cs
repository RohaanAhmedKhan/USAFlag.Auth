using System;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using USAFlag.Auth.Core.Domain.Constants;

namespace USAFlag.Auth.Core.Infrastructure.Persistence.Cache
{
    public interface ICachedConfigurationService
    {
        Task SetCachedValue<T>(string keyName, T value,
            string defaultProviderName = CacheConstant.InMemoryCache);
        Task<T> GetCachedValue<T>(string keyName,
            string defaultProviderName = CacheConstant.InMemoryCache);
    }

    public class CachedConfigurationService : ICachedConfigurationService
    {
        private readonly TimeSpan defaultCachEventme;
        private readonly IConfiguration configuration;
        private readonly IEasyCachingProviderFactory cachingProviderFactory;

        public CachedConfigurationService(IConfiguration configuration,
            IEasyCachingProviderFactory cachingProviderFactory)
        {
            defaultCachEventme = TimeSpan.FromDays(1);
            this.configuration = configuration;
            this.cachingProviderFactory = cachingProviderFactory;
        }

        public async Task SetCachedValue<T>(string keyName, T value,
            string defaultProviderName = CacheConstant.InMemoryCache)
        {
            if (cachingProviderFactory == null)
                return;

            var provider = cachingProviderFactory.GetCachingProvider(defaultProviderName);
            await provider.SetAsync(keyName, value,
               defaultCachEventme);
        }

        public async Task<T> GetCachedValue<T>(string keyName,
            string defaultProviderName = CacheConstant.InMemoryCache)
        {
            if (cachingProviderFactory == null)
                return default;

            string cacheKey = string.Format("{0}", keyName);
            var provider = cachingProviderFactory.GetCachingProvider(defaultProviderName);
            var cacheValue = await provider.GetAsync<T>(cacheKey);

            if (cacheValue.HasValue)
            {
                return cacheValue.Value;
            }

            return default;
        }
    }
}

