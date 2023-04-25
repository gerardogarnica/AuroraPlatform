using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class UserToken : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public DateTime IssuedDate { get; set; }
        public User User { get; set; }
    }
}