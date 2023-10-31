using Aurora.Framework;

namespace Aurora.Platform.Security.Domain.Exceptions
{
    public class ApplicationException : BusinessException
    {
        public ApplicationException(string message)
        : base("ApplicationException", message) { }
    }

    public class InvalidApplicationCodeException : ApplicationException
    {
        public InvalidApplicationCodeException(string code)
            : base($"The application code '{code}' does not exist.") { }
    }
}