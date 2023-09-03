// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

Console.WriteLine("Hello, World!");
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
IDatabase db = redis.GetDatabase();
db.StringSet("mykey", "myvalue");
string value = db.StringGet("mykey");
Console.WriteLine(value);
// List operations
db.ListLeftPush("mylist", "value1");
db.ListLeftPush("mylist", "value2");
RedisValue[] listValues = db.ListRange("mylist");
Console.WriteLine(listValues.ToList().Aggregate((i,j)=>i.ToString()+j.ToString()));
// Set operations
db.SetAdd("myset", "value1");
db.SetAdd("myset", "value2");
RedisValue[] setValues = db.SetMembers("myset");

// Hash operations
db.HashSet("myhash", "field1", "value1");
db.HashSet("myhash", "field2", "value2");
RedisValue value1 = db.HashGet("myhash", "field1");
RedisValue value2 = db.HashGet("myhash", "field2");
Console.WriteLine(value1);
Console.WriteLine(value2);
Console.ReadLine();
