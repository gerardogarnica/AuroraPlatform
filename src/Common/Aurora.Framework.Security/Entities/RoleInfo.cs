using System.Security.Claims;

namespace Aurora.Framework.Security
{
    public class RoleInfo
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsActive { get; set; }

        internal string InternalRoleData
        {
            get
            {
                return string.Concat(
                    RoleId, ";",
                    Name, ";",
                    Description, ";",
                    IsGlobal, ";",
                    IsActive);
            }
        }

        public RoleInfo() { }

        internal RoleInfo(Claim claim)
        {
            var internalDataTokens = claim.Value.Split(";");

            RoleId = int.Parse(internalDataTokens[0]);
            Name = internalDataTokens[1];
            Description = internalDataTokens[2];
            IsGlobal = internalDataTokens[3].ToBoolean().Value;
            IsActive = internalDataTokens[4].ToBoolean().Value;
        }
    }
}