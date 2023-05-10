using Aurora.Framework;

namespace Aurora.Platform.Security.Domain.Exceptions
{
    public class UserException : BusinessException
    {
        protected const string UserNullMessage = "The record of the user cannot be null.";
        protected const string InactiveUserMessage = "The user {0} is not active.";
        protected const string InvalidCredentialsMessage = "The username or password are incorrect.";
        protected const string InvalidUserNameMessage = "The username '{0}' does not exist.";
        protected const string PasswordExpiredMessage = "The user password has expired. Password must be changed before login.";

        public UserException(string message)
            : base("UserException", message) { }
    }

    public class UserNullException : UserException
    {
        public UserNullException()
            : base(UserNullMessage) { }
    }

    public class InvalidCredentialsException : UserException
    {
        public InvalidCredentialsException()
            : base(InvalidCredentialsMessage) { }
    }

    public class InactiveUserException : UserException
    {
        public InactiveUserException(string loginName)
            : base(string.Format(InactiveUserMessage, loginName)) { }
    }

    public class InvalidUserNameException : UserException
    {
        public InvalidUserNameException(string loginName)
            : base(string.Format(InvalidUserNameMessage, loginName)) { }
    }

    public class PasswordExpiredException : UserException
    {
        public PasswordExpiredException()
            : base(PasswordExpiredMessage) { }
    }
}