namespace Ocdata.Operations.Entities
{
    public sealed class MongoConfig : IMongoConfig
    {
        public string? MongoClient { get; private set; }
        public string? Database { get; private set; }

        public void SetMongoConfig(string? mongoClient, string? database)
        {
            MongoClient = mongoClient;
            Database = database;
        }

        public string? GetMongoClient()
        {
            return MongoClient;
        }

        public string? GetDatabase()
        {
            return Database;
        }
    }
}
