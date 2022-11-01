namespace Ocdata.Operations.Repositories.Contracts
{
    public interface IMongoUnitOfWork : IDisposable
    {
        IMongoRepository<T> Repository<T>() where T : class;
        Task<bool> Commit();
    }
}
