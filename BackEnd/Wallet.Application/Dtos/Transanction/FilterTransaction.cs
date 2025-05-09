namespace Wallet.Application.Dtos.Transanction
{
    public class FilterTransaction
    {
        public Guid WalletId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
