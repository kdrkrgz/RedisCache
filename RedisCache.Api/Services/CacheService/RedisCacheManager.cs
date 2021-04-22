using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Api.Services.CacheService
{
    public class RedisCacheManager : ICacheService
    {
        private readonly RedisServer _redisServer;
        public RedisCacheManager(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object data, int keyExpire)
        {
            string cacheData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, cacheData);
            _redisServer.Database.KeyExpire(key,TimeSpan.FromSeconds(keyExpire));
        }

        public void Clear()
        {
            _redisServer.FlushDataBase();
        }

        public T Get<T>(string key)
        {
            if (IsAdd(key))
            {
                string cacheData = _redisServer.Database.StringGet(key);
                return JsonConvert.DeserializeObject<T>(cacheData);
            }
            return default;
        }


        public object Get(string key)
        {
            if (IsAdd(key))
            {
                string cacheData = _redisServer.Database.StringGet(key);
                return JsonConvert.DeserializeObject(cacheData);
            }
            return null;
        }

        public bool IsAdd(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public void Remove(string key)
        {
            if (IsAdd(key))
            {
                _redisServer.Database.KeyDelete(key);
            }
        }
    }
}
