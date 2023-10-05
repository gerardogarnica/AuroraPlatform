using Aurora.Framework;

namespace Aurora.Platform.Security.Domain.Exceptions
{
    public class UserException : BusinessException
    {
        public UserException(string message)
            : base("UserException", message) { }
    }

    public class UserNullException : UserException
    {
        public UserNullException()
            : base("The record of the user cannot be null.") { }
    }

    public class ExpiredUserTokenException : UserException
    {
        public ExpiredUserTokenException()
            : base("The refresh token is expired.") { }
    }

    public class InvalidCredentialsException : UserException
    {
        public InvalidCredentialsException()
            : base("The email or password are incorrect.") { }
    }

    public class InactiveUserException : UserException
    {
        public InactiveUserException(string email)
            : base($"The user '{email}' is not active.") { }
    }

    public class InvalidUserEmailException : UserException
    {
        public InvalidUserEmailException(string email)
            : base($"The user email '{email}' does not exist.") { }
    }

    public class InvalidUserGuidException : UserException
    {
        public InvalidUserGuidException(string guid)
            : base($"The user guid '{guid}' does not exist.") { }
    }

    public class InvalidUserTokenException : UserException
    {
        public InvalidUserTokenException()
            : base("The refresh token is invalid.") { }
    }

    public class PasswordExpiredException : UserException
    {
        public PasswordExpiredException()
            : base("The user password has expired. Password must be changed before login.") { }
    }

    public class UnableChangeUserException : UserException
    {
        public UnableChangeUserException()
            : base("The user is unable to be changed.") { }
    }

    public class UserDoesNotHaveRoleException : UserException
    {
        public UserDoesNotHaveRoleException(string email, string roleName)
            : base($"The user '{email}' does not have the '{roleName}' role.") { }
    }
}