using Microsoft.EntityFrameworkCore;
using Wallet.Application.Dtos.Transanction;
using Wallet.Application.Models;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.Business
{
    public class TransactionBusiness
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionBusiness(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        

        public async Task<PagedResult<TransactionDto>> GetAllSentTransactionsAsync(FilterTransaction filter)
        {
            var query = _transactionRepository.GetAll();

            query = query.Where(t => t.SenderWalletId == filter.WalletId);

            if (filter.StartDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt.Date >= filter.StartDate.Value.ToUniversalTime().Date);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt.Date <= filter.EndDate.Value.ToUniversalTime().Date);
            }

            var totalItens = await query.CountAsync();

            var itens = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Include(t => t.ReceiverWallet)
                    .ThenInclude(w => w.User)
                .Take(filter.PageSize)
                .Select(t => new TransactionDto
                {
                    Amount = t.Amout,
                    Data = t.CreatedAt,
                    Recipient = t.ReceiverWallet.User.Name
                })
                .ToListAsync();

            return new PagedResult<TransactionDto>
            {
                Itens = itens,
                TotalItems = totalItens,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPage = (int)Math.Ceiling((double)totalItens / filter.PageSize)
            };
        }

        public async Task<PagedResult<TransactionDto>> GetAllRecipientTransactionsAsync(FilterTransaction filter)
        {
            var query = _transactionRepository.GetAll();

            query = query.Where(t => t.ReceiverWalletId == filter.WalletId);

            if (filter.StartDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt.Date >= filter.StartDate.Value.ToUniversalTime().Date);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt.Date <= filter.EndDate.Value.ToUniversalTime().Date);
            }

            var totalItens =  await query.CountAsync();

            var itens = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Include(t => t.SenderWallet)
                    .ThenInclude(w => w.User)
                .Take(filter.PageSize)
                .Select(t => new TransactionDto
                {
                    Amount = t.Amout,
                    Data = t.CreatedAt,
                    Recipient = t.SenderWallet.User.Name
                })
                .ToListAsync();

            return new PagedResult<TransactionDto>
            {
                Itens = itens,
                TotalItems = totalItens,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPage = (int)Math.Ceiling((double)totalItens / filter.PageSize)
            };
        }
    }
}
