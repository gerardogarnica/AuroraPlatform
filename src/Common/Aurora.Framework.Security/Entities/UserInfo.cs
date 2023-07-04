using System.Security.Claims;

namespace Aurora.Framework.Security
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName).Trim();
            }
        }
        public bool PasswordMustChange
        {
            get
            {
                return PasswordExpirationDate.HasValue && PasswordExpirationDate.Value.Date <= DateTime.UtcNow.Date;
            }
        }
        public DateTime? PasswordExpirationDate { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public List<RoleInfo> Roles { get; set; }

        internal string InternalUserData
        {
            get
            {
                return string.Concat(
                    PasswordMustChange, ";",
                    PasswordExpirationDate.HasValue ? PasswordExpirationDate.Value.ToString(DateFormat.YearMonthDay) : "", ";",
                    IsDefault, ";",
                    IsActive);
            }
        }

        public UserInfo() { }

        internal UserInfo(List<Claim> claims)
        {
            UserId = int.Parse(claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid)).Value);
            FirstName = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.GivenName)).Value;
            LastName = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Surname)).Value;
            Email = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email)).Value;

            var internalDataTokens = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData)).Value.Split(";");

            PasswordExpirationDate = internalDataTokens[1].ToDateTime("yyyyMMdd");
            IsDefault = internalDataTokens[2].ToBoolean().Value;
            IsActive = internalDataTokens[3].ToBoolean().Value;
            Roles = (from claim in claims.Where(x => x.Type.Equals(ClaimTypes.Role))
                     select new RoleInfo(claim)).ToList();
        }
    }
}