using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisTest1
{
    public static class CommationManager
    {

        public static IDatabase CreateAnouyConation()
        {
            IDatabase db = GetRedisDatabase("172.23.166.71:9091,proxy=envoyproxy");
            return db;
        }

        public static IDatabase CreateInstancConation()
        {
            IDatabase db =
                GetRedisDatabase("172.23.166.71:6379,172.23.166.72:6379,172.23.166.73:6379,172.23.166.74:6379,172.23.166.75:6379,172.23.166.76:6379");
            return db;
        }
        public static IDatabase GetRedisDatabase(string connection)
        {
            ConfigurationOptions configuration = ConfigurationOptions.Parse(connection);
            configuration.AbortOnConnectFail = false;
            configuration.AllowAdmin = true;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
            return redis.GetDatabase();
        }

    }
}
