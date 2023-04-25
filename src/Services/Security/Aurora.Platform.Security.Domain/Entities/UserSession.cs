using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class UserSession : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public DateTime BeginSessionDate { get; set; }
        public DateTime? EndSessionDate { get; set; }
    }
}