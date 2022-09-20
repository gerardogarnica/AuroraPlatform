using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class OptionsListException : BusinessException
    {
        protected const string OptionsListNullMessage = "The record of the options list cannot be null.";

        public OptionsListException(string message)
            : base("OptionsListException", message) { }
    }

    public class OptionsListNullException : OptionsListException
    {
        public OptionsListNullException()
            : base(OptionsListNullMessage) { }
    }
}