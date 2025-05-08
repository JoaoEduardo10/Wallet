namespace Wallet.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity?> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        Task<int> SaveChangesAsync();
    }
}
