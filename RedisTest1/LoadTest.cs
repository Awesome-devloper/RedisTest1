﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace RedisTest1
{
    public class LoadTest
    {
        private readonly IDatabase db;
        private readonly Iread
        public LoadTest(IDatabase connection)
        {
            db = connection;
        }
        const string msg = "Hello Aref For Instatans: ";


        public async Task<bool> loadTest(int NumberKeys)
        {

            List<int> keys = Enumerable.Range(1, NumberKeys).ToList();
            int counter = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            // Scenario 1: Create Data on Redis
            Console.WriteLine("Scenario 1: Create Data on Redis");
            await Parallel.ForEachAsync(keys, new ParallelOptions() { MaxDegreeOfParallelism = 20 },
            (item, cancelToken) =>
            {
                Interlocked.Increment(ref counter);
                if (counter % 100000 == 0)
                    Console.WriteLine("Write " + counter + " time :" + stopwatch.ElapsedMilliseconds / 1000+ "sec");

                return SetValueAsync(item);
            });
            stopwatch.Stop();
            Console.WriteLine($"Scenario 1: Fnish Frist Step {stopwatch.ElapsedMilliseconds / 1000} sec");
            stopwatch.Restart();
            // Scenario 2: Read Data from Redis
            Console.WriteLine("Scenario 2: Read Data from Redis");
            counter = 0;
            await Parallel.ForEachAsync(keys, new ParallelOptions() { MaxDegreeOfParallelism = 20 },
            (item, cancelToken) =>
            {
                Interlocked.Increment(ref counter);
                if (counter % 100000 == 0)
                    Console.WriteLine("Read " + counter + " time :" + stopwatch.ElapsedMilliseconds / 1000 + "sec");

                return GetValueAsync(item);
            });
            stopwatch.Stop();
            Console.WriteLine($"Scenario 2: Fnish Frist Step {stopwatch.ElapsedMilliseconds / 1000} sec");
            return true;
        }


 
    }
}
