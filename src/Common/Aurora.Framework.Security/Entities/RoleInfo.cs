using System.Security.Claims;

namespace Aurora.Framework.Security
{
    public class RoleInfo
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string AppCode { get; set; }
        public string AppName { get; set; }
        public string Description { get; set; }
        public Guid Guid { get; set; }
        public string Notes { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        internal string InternalRoleData
        {
            get
            {
                return string.Concat(
                    RoleId, ";",
                    Name, ";",
                    AppCode, ";",
                    AppName, ";",
                    Description, ";",
                    Guid.ToString(), ";",
                    Notes, ";",
                    IsDefault, ";",
                    IsActive);
            }
        }

        public RoleInfo() { }

        internal RoleInfo(Claim claim)
        {
            var internalDataTokens = claim.Value.Split(";");

            RoleId = int.Parse(internalDataTokens[0]);
            Name = internalDataTokens[1];
            AppCode = internalDataTokens[2];
            AppName = internalDataTokens[3];
            Description = internalDataTokens[4];
            Guid = new Guid(internalDataTokens[5]);
            Notes = internalDataTokens[6];
            IsDefault = internalDataTokens[7].ToBoolean().Value;
            IsActive = internalDataTokens[8].ToBoolean().Value;
        }
    }
}