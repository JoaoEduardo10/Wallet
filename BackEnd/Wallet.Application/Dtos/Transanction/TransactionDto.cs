namespace Wallet.Application.Dtos.Transanction
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public DateTime Data {  get; set; }
        public string Recipient { get; set; } = string.Empty;
    }
}
