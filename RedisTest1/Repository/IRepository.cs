namespace RedisTest1.Repository
{
    public interface IRepository
    {
        public ValueTask SetValueAsync(int item, string msg);
        public ValueTask GetValueAsync(int item, string msg);
        public ValueTask SetAndGetValueAsync(int item, string msg);
    }
}
