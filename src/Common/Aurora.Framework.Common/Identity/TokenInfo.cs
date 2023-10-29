namespace Aurora.Framework.Identity
{
    public class TokenInfo : IdentityToken
    {
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}