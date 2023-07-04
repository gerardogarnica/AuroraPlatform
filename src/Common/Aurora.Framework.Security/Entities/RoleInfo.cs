using System.Security.Claims;

namespace Aurora.Framework.Security
{
    public class RoleInfo
    {
        public int RoleId { get; set; }
        public string Application { get; set; }
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
                    Application, ";",
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
            Application = internalDataTokens[1];
            Name = internalDataTokens[2];
            Description = internalDataTokens[3];
            IsGlobal = internalDataTokens[4].ToBoolean().Value;
            IsActive = internalDataTokens[5].ToBoolean().Value;
        }
    }
}