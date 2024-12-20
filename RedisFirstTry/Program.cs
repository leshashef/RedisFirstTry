//docker container run --name redis-container -p 6379:6379 -d redis

//запускаем контейнер и можно работать

using StackExchange.Redis;
using System.Diagnostics;

var redis = ConnectionMultiplexer.Connect("localhost");
var db = redis.GetDatabase();

Random random = new Random();

for(int x = 0; x < 10000; x++)
{
    db.ListRightPush("listOfNumber", random.Next());
    
}

Stopwatch stopwatch = Stopwatch.StartNew();
var list = db.ListRange("listOfNumber");
stopwatch.Stop();
foreach (var item in list.TakeLast(5))
{
    Console.WriteLine(item);
}

Console.WriteLine("time to get list: " +  stopwatch.ElapsedMilliseconds + " ms");

//множества
db.SetAdd("mySet", "value1");
db.SetAdd("mySet", "value2");

var set = db.SetMembers("mySet");
foreach (var member in set)
{
    Console.WriteLine(member);
}

//хеши
db.HashSet("myHash", "field1", "value1");
db.HashSet("myHash", "field2", "value2");

var hashEntries = db.HashGetAll("myHash");
foreach (var entry in hashEntries)
{
    Console.WriteLine($"{entry.Name}: {entry.Value}");
}