using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace RedisTest1
{
    public class RandomData
    {
        private readonly IDatabase db;
        public RandomData(IDatabase connection)
        {
            db = connection;
        }
        public async Task UpdateData(Int32 arrayLength)
        {
            int numberOfKey = arrayLength/100;
            int counter = 0;
            Dictionary<int, string> value = CreateDictionary(arrayLength, numberOfKey);
            await Parallel.ForEachAsync(value, new ParallelOptions() { MaxDegreeOfParallelism = 40 },
            (item, cancelToken) =>
            {
                Interlocked.Increment(ref counter);
                if (counter % 1000 == 0)
                    Console.WriteLine("UpdateWrite " + counter);

                return SetValueAsync(item.Key,item.Value);
            });
            Console.WriteLine("Finsh All Update Exactlly");
        }
        private Dictionary<int, string> CreateDictionary(int arrayLength, int numberOfKey)
        {
            int length = arrayLength;
            Dictionary<int, string> keyValues = new Dictionary<int, string>();
            List<int> keys = Enumerable.Range(1, arrayLength).ToList();
            for (int i = 0; i < numberOfKey; i++)
            {
                Random random = new Random();
                int key = random.Next(0, length);
                keyValues.Add(keys[key], Guid.NewGuid().ToString());
                keys.RemoveAt(key);
                length--;
            }

            return keyValues;

        }
        private async ValueTask SetValueAsync(int item, string message)
        {
            await db.StringSetAsync(item.ToString(), message);
            var result = await db.StringGetAsync(item.ToString());
            Assert.AreEqual<string>(message, result);
        }
    }
}
