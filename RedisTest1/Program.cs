using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisTest1;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Channels;

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
        LoadTest loadTest = new LoadTest(db);
        RandomData randUpdate = new RandomData(db);
        await Console.Out.WriteLineAsync("Plaese Enter Number of keys");
        _ = int.TryParse(Console.ReadLine(), out var number);

        await loadTest.loadTest(number);
        await randUpdate.UpdateData(number);

        Console.WriteLine("Completed");
        Console.ReadLine();
    }

}