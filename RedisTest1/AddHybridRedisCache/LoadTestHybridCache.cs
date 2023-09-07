using HybridRedisCache;

namespace RedisTest1.AddHybridRedisCache
{
    public class LoadTestHybridCache
    {
        private readonly HybridCache _cache;
        public LoadTestHybridCache(HybridCache cache)
        {
            // Create a new instance of HybridCache with cache options
            _cache = cache;
            
        }

    }
}
