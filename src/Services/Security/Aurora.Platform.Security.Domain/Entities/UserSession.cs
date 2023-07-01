﻿using Aurora.Framework.Entities;
using Aurora.Framework.Security;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class UserSession : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public DateTime BeginSessionDate { get; set; }
        public DateTime? EndSessionDate { get; set; }

        public void UpdateWithTokenInfo(TokenInfo tokenInfo)
        {
            AccessToken = tokenInfo.AccessToken;
            AccessTokenExpiration = tokenInfo.AccessTokenExpiration;
            RefreshToken = tokenInfo.RefreshToken;
            RefreshTokenExpiration = tokenInfo.RefreshTokenExpiration;
        }
    }
}