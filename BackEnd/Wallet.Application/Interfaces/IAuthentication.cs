using Wallet.Application.Models;
using Wallet.Domain.Utilities;

namespace Wallet.Application.Interfaces
{
    public interface IAuthentication
    {
        Task<Result<Authentication>> GetAuthenticationAsync(Guid userId);
    }
}
