using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Wallet.Domain.Utilities;

namespace Wallet.Domain.Entities
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;  
        public Wallet Wallet { get; set; } 
        public Guid WalletId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User()
        {
            Wallet = new Wallet();
        }

        public Result ValidateUser()
        {
            var result = new Result();

            if (string.IsNullOrWhiteSpace(Name))
            {
                result.AddError("O nome é obrigatório.");
            }

            if (Name.Length < 3)
            {
                result.AddError("O nome deve ter pelo menos 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                result.AddError("O e-mail é obrigatório.");
            }

            if (!IsEmailValid())
            {
                result.AddError("O e-mail é inválido.");
            }

            if (string.IsNullOrEmpty(Password))
            {
                result.AddError("A senha é obrigatória.");
            }

            if (Password.Length < 8)
            {
                result.AddError("A senha deve conter pelo menos 8 caracteres.");
            }

            return result;

        }

        public void HashPassword()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); 

            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            Password = Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }

        private bool IsEmailValid()
        {
            try
            {
                var addr = new MailAddress(Email.Trim());
                return string.Equals(addr.Address, Email.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
