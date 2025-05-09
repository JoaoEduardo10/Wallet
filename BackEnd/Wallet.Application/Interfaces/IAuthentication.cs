using Wallet.Application.Models;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Utilities;

namespace Wallet.Application.Interfaces
{
    public interface IAuthentication
    {
        Result<Authentication> GetAuthentication(Guid userId, string name);
    }
}
