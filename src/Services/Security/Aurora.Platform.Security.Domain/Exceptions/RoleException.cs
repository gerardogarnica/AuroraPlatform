using Aurora.Framework;

namespace Aurora.Platform.Security.Domain.Exceptions
{
    public class RoleException : BusinessException
    {
        public RoleException(string message)
            : base("RoleException", message) { }
    }

    public class RoleNotExistsException : RoleException
    {
        public RoleNotExistsException()
            : base("The role does not exist.") { }
    }

    public class InvalidRoleIdentifierException : RoleException
    {
        public InvalidRoleIdentifierException(int roleId)
            : base($"The role ID '{roleId}' does not exist.") { }
    }

    public class InactiveRoleException : RoleException
    {
        public InactiveRoleException(string name)
            : base($"The role '{name}' is not active.") { }
    }

    public class RoleNameAlreadyExistsException : RoleException
    {
        public RoleNameAlreadyExistsException(string name, string application)
            : base($"The role name '{name}' already exists in the application '{application}'.") { }
    }
}