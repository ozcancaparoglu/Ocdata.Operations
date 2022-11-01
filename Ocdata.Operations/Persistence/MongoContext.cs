using MongoDB.Driver;
using Ocdata.Operations.Entities;

namespace Ocdata.Operations.Persistence
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }

        public IClientSessionHandle Session { get; set; }

        public MongoClient MongoClient { get; set; }

        private readonly List<Func<Task>> _commands;

        private readonly IMongoConfig _config;

        public MongoContext(IMongoConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            
            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
                return;

            MongoClient = new MongoClient(_config.GetMongoClient());
            Database = MongoClient.GetDatabase(_config.GetDatabase());
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}
