using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Redis
{
    public class RedisCache
    {
        private readonly IConfiguration configuration;
        private IDatabase db;
        public RedisCache(
            IConfiguration configuration
            )
        {
            this.configuration = configuration;
            var conn = this.configuration.GetSection("Redis:Connection").Value;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conn);
            db = redis.GetDatabase();
        }

        public void SetString(string key, string value, int expireSecond)
        {
            db.StringSet(key, value, TimeSpan.FromSeconds(expireSecond));
        }

        public string GetString(string key)
        {
            return db.StringGet(key);
        }
    }
}
