using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace RedisTest1.Repository
{
    public class RedisCacheRepository : IRepository
    {
        private readonly IDatabase _db;
        public RedisCacheRepository(IDatabase db)
        {
            _db = db;
        }
        public async ValueTask SetValueAsync(int item,string msg)
        {
            await _db.StringSetAsync(item.ToString(), msg + item);
        }

        public async ValueTask GetValueAsync(int item, string msg)
        {
            var value = await _db.StringGetAsync(item.ToString());
            var actual = msg + item.ToString();
            Assert.AreEqual<string>(actual, value);
        }
    }
}
