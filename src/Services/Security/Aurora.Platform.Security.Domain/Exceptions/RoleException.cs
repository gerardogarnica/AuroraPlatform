using Aurora.Framework;

namespace Aurora.Platform.Security.Domain.Exceptions
{
    public class RoleException : BusinessException
    {
        protected const string RoleNotExistsMessage = "The role does not exist.";
        protected const string InactiveRoleMessage = "The role '{0}' is not active.";
        protected const string InvalidRoleIdentifierMessage = "The role ID '{0}' does not exist.";

        public RoleException(string message)
            : base("RoleException", message) { }
    }

    public class RoleNotExistsException : RoleException
    {
        public RoleNotExistsException()
            : base(RoleNotExistsMessage) { }
    }

    public class InvalidRoleIdentifierException : RoleException
    {
        public InvalidRoleIdentifierException(int roleId)
            : base(string.Format(InvalidRoleIdentifierMessage, roleId)) { }
    }

    public class InactiveRoleException : RoleException
    {
        public InactiveRoleException(string name)
            : base(string.Format(InactiveRoleMessage, name)) { }
    }
}