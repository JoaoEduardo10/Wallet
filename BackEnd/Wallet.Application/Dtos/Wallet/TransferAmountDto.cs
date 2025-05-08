namespace Wallet.Application.Dtos.Wallet
{
    public class TransferAmountDto
    {
        public Guid SenderWalletId { get; set; }
        public string ReceiverEmail { get; set; }
        public decimal Amount { get; set; }
    }
}
