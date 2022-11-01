using Ocdata.Operations.Entities;

namespace Ocdata.Operations.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        IAsyncRepository<T> Repository<T>() where T : EntityBase;
        Task<int> CommitAsync();
        void Rollback();
    }
}
