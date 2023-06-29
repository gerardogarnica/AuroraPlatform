using Aurora.Framework;

namespace Aurora.Platform.Security.Domain.Exceptions
{
    public class UserException : BusinessException
    {
        protected const string UserNullMessage = "The record of the user cannot be null.";
        protected const string ExpiredUserTokenMessage = "The refresh token is expired.";
        protected const string InactiveUserMessage = "The user {0} is not active.";
        protected const string InvalidCredentialsMessage = "The email or password are incorrect.";
        protected const string InvalidEmailMessage = "The user email '{0}' does not exist.";
        protected const string InvalidUserTokenMessage = "The refresh token is invalid.";
        protected const string PasswordExpiredMessage = "The user password has expired. Password must be changed before login.";
        protected const string UnableChangeUserMessage = "The user is unable to be changed.";

        public UserException(string message)
            : base("UserException", message) { }
    }

    public class UserNullException : UserException
    {
        public UserNullException()
            : base(UserNullMessage) { }
    }

    public class ExpiredUserTokenException : UserException
    {
        public ExpiredUserTokenException()
            : base(ExpiredUserTokenMessage) { }
    }

    public class InvalidCredentialsException : UserException
    {
        public InvalidCredentialsException()
            : base(InvalidCredentialsMessage) { }
    }

    public class InactiveUserException : UserException
    {
        public InactiveUserException(string email)
            : base(string.Format(InactiveUserMessage, email)) { }
    }

    public class InvalidUserNameException : UserException
    {
        public InvalidUserNameException(string email)
            : base(string.Format(InvalidEmailMessage, email)) { }
    }

    public class InvalidUserTokenException : UserException
    {
        public InvalidUserTokenException()
            : base(InvalidUserTokenMessage) { }
    }

    public class PasswordExpiredException : UserException
    {
        public PasswordExpiredException()
            : base(PasswordExpiredMessage) { }
    }

    public class UnableChangeUserException : UserException
    {
        public UnableChangeUserException()
            : base(UnableChangeUserMessage) { }
    }
}