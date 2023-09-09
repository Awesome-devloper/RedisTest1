using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisTest1;
using RedisTest1.Repository;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{


    static async Task Main(string[] args)
    {

        //IDatabase directDb = GetRedisDatabase("172.23.166.71:9091,proxy=envoyproxy");
        //IDatabase db = GetRedisDatabase("172.23.166.71:9091,proxy=envoyproxy");

        // db.Execute("FLUSHDB");


        IDatabase db = CommationManager.CreateAnouyConation();
        IDatabase directDb = CommationManager.CreateInstancConation();
        directDb.Execute("FLUSHDB");
        ///loadTest
        ///
        await Console.Out.WriteLineAsync("for distrbation mode plaese press 2");

        var mode=Console.ReadLine();
        LoadTest loadTest;
        RandomData randUpdate;
        if (mode == "1")
        {
            var redisCacheRepository = new RedisCacheRepository(db);
            loadTest = new LoadTest(redisCacheRepository);
            randUpdate = new RandomData(redisCacheRepository);
        }
        else
        {

            var hybridCarch = new HybridCarchRepository(CommationManager.hybridCache());
            loadTest = new LoadTest(hybridCarch);
            randUpdate = new RandomData(hybridCarch);
        }
        await Console.Out.WriteLineAsync("Plaese Enter Number of keys");
        _ = int.TryParse(Console.ReadLine(), out var number);
        await loadTest.loadTest(number);
        await randUpdate.UpdateData(number);
        Console.WriteLine("Completed");
        Console.ReadLine();
    }

}