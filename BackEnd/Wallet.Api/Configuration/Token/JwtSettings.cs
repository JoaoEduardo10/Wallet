namespace Wallet.Api.Configuration.Token
{
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public int Lifespan { get; set; }
    }
}
