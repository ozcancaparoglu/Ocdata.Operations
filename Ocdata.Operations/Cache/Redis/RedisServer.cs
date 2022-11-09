using Microsoft.Extensions.Options;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Ocdata.Operations.Cache.Redis
{
    public class RedisServer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly RedisConfigurationOptions _configuration;
        private readonly IDatabase _database;

        private string configurationString;
        private readonly int _currentDatabaseId = 0;
        
        public RedisServer(IOptions<RedisConfigurationOptions> options)
        {
            _configuration = options.Value;
            CreateRedisConfigurationString();

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration: configurationString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        public IDatabase Database => _database;

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(configurationString).FlushDatabase(_currentDatabaseId);
        }

        private void CreateRedisConfigurationString()
        {
            configurationString = $"{_configuration.Host}:{_configuration.Port},{_configuration.Admin}";
        }
    }
}
