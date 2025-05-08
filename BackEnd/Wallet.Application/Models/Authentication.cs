namespace Wallet.Application.Models
{
    public class Authentication
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; }
        public long TokenExpirationTime { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
