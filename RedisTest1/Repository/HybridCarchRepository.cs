using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridRedisCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace RedisTest1.Repository
{
    public class HybridCarchRepository : IRepository
    {
        private readonly HybridCache _db;
        public HybridCarchRepository(HybridCache db)
        {
                _db = db;
        }
        public async ValueTask GetValueAsync(int item, string msg)
        {
            await _db.SetAsync(item.ToString(), msg + item);
        }



        public async ValueTask SetValueAsync(int item, string msg)
        {

            var value = await _db.GetAsync<string>(item.ToString());
            var actual = msg + item.ToString();
            Assert.AreEqual<string>(actual, value);
        }
        public async ValueTask SetAndGetValueAsync(int item, string msg)
        {
            await _db.SetAsync(item.ToString(), msg);
            var result = await _db.GetAsync<string>(item.ToString());
            Assert.AreEqual<string>(msg, result);

        }
    }
}
