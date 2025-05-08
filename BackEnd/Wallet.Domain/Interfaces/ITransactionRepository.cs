using Wallet.Domain.Entities;

namespace Wallet.Domain.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
      IQueryable<Transaction> GetAllWithCollections();
    }
}
