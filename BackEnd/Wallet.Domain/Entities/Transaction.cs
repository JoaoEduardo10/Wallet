using Wallet.Domain.Entities.Enums;
using Wallet.Domain.Utilities;

namespace Wallet.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid SenderWallerId { get; set; }
        public Wallet SenderWallet { get; set; }
        public Guid ReceiverWallerId { get; set; }
        public Wallet ReceiverWaller { get; set; }
        public decimal Amout { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Transaction()
        {
            SenderWallet = new Wallet();
            ReceiverWaller = new Wallet();
        }

        public Result ValidateTransaction()
        {
            var result = new Result();

            if (SenderWallerId == Guid.Empty)
                result.AddError("Carteira remetente inválida.");

            if (ReceiverWallerId == Guid.Empty)
                result.AddError("Carteira destinatária inválida.");

            if (SenderWallerId == ReceiverWallerId)
                result.AddError("A carteira remetente não pode ser igual à destinatária.");

            if (Amout <= 0)
                result.AddError("O valor da transação deve ser maior que zero.");

            if (!Enum.IsDefined(typeof(TransactionStatus), Status))
                result.AddError("Status da transação inválido.");

            if (CreatedAt == default)
                result.AddError("Data da transação inválida.");

            return result;
        }
    }
}
