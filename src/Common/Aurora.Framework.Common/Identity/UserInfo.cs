using System.Security.Claims;

namespace Aurora.Framework.Identity
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
        public Guid Guid { get; set; }
        public bool PasswordMustChange
        {
            get
            {
                return PasswordExpirationDate.HasValue && PasswordExpirationDate.Value.Date <= DateTime.UtcNow.Date;
            }
        }
        public DateTime? PasswordExpirationDate { get; set; }
        public string Notes { get; set; }
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
                    Notes, ";",
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
            Guid = new Guid(claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.SerialNumber)).Value);

            var internalDataTokens = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData)).Value.Split(";");

            PasswordExpirationDate = internalDataTokens[1].ToDateTime("yyyyMMdd");
            Notes = internalDataTokens[2];
            IsDefault = internalDataTokens[3].ToBoolean().Value;
            IsActive = internalDataTokens[4].ToBoolean().Value;
            Roles = (from claim in claims.Where(x => x.Type.Equals(ClaimTypes.Role))
                     select new RoleInfo(claim)).ToList();
        }
    }
}