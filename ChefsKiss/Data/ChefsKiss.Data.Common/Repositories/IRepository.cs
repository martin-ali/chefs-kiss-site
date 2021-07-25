namespace ChefsKiss.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        int Count { get; }

        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
