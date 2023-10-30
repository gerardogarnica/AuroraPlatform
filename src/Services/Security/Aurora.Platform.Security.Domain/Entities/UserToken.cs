using Aurora.Framework.Entities;
using Aurora.Framework.Identity;
using Aurora.Platform.Security.Domain.Exceptions;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class UserToken : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public string Application { get; set; }
        public string AccessToken { get; set; }
        public DateTime? AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public DateTime? IssuedDate { get; set; }
        public User User { get; set; }

        public void CheckIfRefreshTokenIsValid(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(RefreshToken)) throw new InvalidUserTokenException();

            if (refreshToken == RefreshToken) return;

            throw new InvalidUserTokenException();
        }

        public void CheckIfRefreshTokenIsNotExpired()
        {
            if (!RefreshTokenExpiration.HasValue) throw new ExpiredUserTokenException();

            if (RefreshTokenExpiration.Value > DateTime.UtcNow) return;

            throw new ExpiredUserTokenException();
        }

        public void UpdateWithTokenInfo(TokenInfo tokenInfo)
        {
            AccessToken = tokenInfo.AccessToken;
            AccessTokenExpiration = tokenInfo.AccessTokenExpiration;
            RefreshToken = tokenInfo.RefreshToken;
            RefreshTokenExpiration = tokenInfo.RefreshTokenExpiration;
            IssuedDate = DateTime.UtcNow;
        }

        public void ClearTokenInfo()
        {
            AccessToken = null;
            AccessTokenExpiration = null;
            RefreshToken = null;
            RefreshTokenExpiration = null;
        }
    }
}