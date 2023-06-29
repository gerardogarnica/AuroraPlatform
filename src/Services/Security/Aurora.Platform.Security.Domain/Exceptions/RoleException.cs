using Aurora.Framework;

namespace Aurora.Platform.Security.Domain.Exceptions
{
    public class RoleException : BusinessException
    {
        protected const string RoleNotExistsMessage = "The role does not exist.";

        public RoleException(string message)
            : base("RoleException", message) { }
    }

    public class RoleNotExistsException : RoleException
    {
        public RoleNotExistsException()
            : base(RoleNotExistsMessage) { }
    }
}