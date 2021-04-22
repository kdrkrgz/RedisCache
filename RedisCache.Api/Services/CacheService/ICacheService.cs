using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Api.Services.CacheService
{
    public interface ICacheService
    {
        void Add(string key, object data, int keyExpire);
        T Get<T>(string key);
        object Get(string key);
        void Remove(string key);
        bool IsAdd(string key);
        void Clear();
    }
}
