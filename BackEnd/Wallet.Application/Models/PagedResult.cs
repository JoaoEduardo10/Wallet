namespace Wallet.Application.Models
{
    public class PagedResult<TEntity>
    {
        public List<TEntity> Itens { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
