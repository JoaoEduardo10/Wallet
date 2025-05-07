using System.ComponentModel;

namespace Wallet.Domain.Entities.Enums
{
    public enum TransactionStatus : int
    {
        [Description("Pendente")]
        Pending = 1,

        [Description("Concluída")]
        Completed = 2,

        [Description("Falhou")]
        Failed = 3,

        [Description("Cancelada")]
        Cancelled = 4

    }
}
