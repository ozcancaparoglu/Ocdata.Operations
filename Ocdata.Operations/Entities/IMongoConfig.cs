namespace Ocdata.Operations.Entities
{
    public interface IMongoConfig
    {
        string? GetDatabase();
        string? GetMongoClient();
        void SetMongoConfig(string? mongoClient, string? database);
    }
}