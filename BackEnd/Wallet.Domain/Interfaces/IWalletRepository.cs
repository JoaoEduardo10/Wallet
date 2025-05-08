
namespace Wallet.Domain.Interfaces
{
    public interface IWalletRepository : IGenericRepository<Entities.Wallet> 
    {
        Task<Entities.Wallet?> GetWalletByEmailAsync(string email);
        Task<Entities.Wallet?> GetWalletByUserIdAsync(Guid userId);
        Task<Entities.Wallet?> GetWalletByIdAsync(Guid id);
    }
}
