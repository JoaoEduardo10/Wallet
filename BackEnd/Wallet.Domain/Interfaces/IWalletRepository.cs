
namespace Wallet.Domain.Interfaces
{
    public interface IWalletRepository : IGenericRepository<Wallet.Domain.Entities.Wallet> 
    {
        IQueryable<Wallet.Domain.Entities.Wallet> GetAllWithCollections();
    }
}
