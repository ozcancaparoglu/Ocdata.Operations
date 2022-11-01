using Microsoft.EntityFrameworkCore;
using Ocdata.Operations.Persistence;
using Ocdata.Operations.Repositories.Contracts;

namespace Ocdata.Operations.Repositories
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        private readonly IMongoContext _context;

        private readonly Dictionary<Type, object> _repositories = new();

        public MongoUnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public IMongoRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IMongoRepository<T>;
            }

            IMongoRepository<T> repo = new MongoRepository<T>(_context);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
