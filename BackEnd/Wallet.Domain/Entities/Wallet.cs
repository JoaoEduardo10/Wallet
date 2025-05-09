using Wallet.Domain.Utilities;

namespace Wallet.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Result ValidateWallet()
        {
            var result = new Result();

            if (Balance < 0)
            {
                result.AddError("O saldo não pode ser negativo.");
            }

            if (UserId == Guid.Empty)
            {
                result.AddError("Usuário inválido.");
            }

            return result;
        }

        public Result AddAmount(decimal amount)
        {
            var result = new Result();

            if (amount <= 0)
            {
                result.AddError("O valor não pode ser negativo ou igual a zero.");
            }

            if (Balance + amount < Balance)
            {
                result.AddError("Não foi possível adicionar o valor.");
            }

            Balance += amount;
            UpdatedAt = DateTime.UtcNow;

            return result;
        }

        public Result RemoveAmount(decimal amount)
        {
            var result = new Result();

            if (amount < 0)
            {
                result.AddError("O valor não pode ser negativo.");
            }

            if (amount > Balance)
            {
                result.AddError("Saldo insuficiente para realizar a operação.");
            }

            if (Balance - amount < 0)
            {
                result.AddError("Não foi possivel executar a operação.");
            }

            Balance -= amount;
            UpdatedAt = DateTime.UtcNow;

            return result;
        }

        

        public void CreateWallet()
        {
            const decimal INITIAL_BALANCE = 100.00m;

            Balance = INITIAL_BALANCE;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
