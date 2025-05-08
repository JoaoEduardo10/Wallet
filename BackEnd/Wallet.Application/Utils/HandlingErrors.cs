using Wallet.Domain.Utilities;

namespace Wallet.Application.Utils
{
    public static class HandlingErrors
    {
        public static string FormateErrors(Result result)
        {
            return string.Join(Environment.NewLine, result.Errors);
        }
    }
}
