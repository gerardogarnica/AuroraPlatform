using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class OptionsListException : BusinessException
    {
        protected const string OptionsListNullMessage = "The record of the options list cannot be null.";
        protected const string ExistsOptionsListCodeMessage = "The options list code {0} already exists and cannot be created again.";

        public OptionsListException(string message)
            : base("OptionsListException", message) { }
    }

    public class OptionsListNullException : OptionsListException
    {
        public OptionsListNullException()
            : base(OptionsListNullMessage) { }
    }

    public class ExistsOptionsListCodeException : OptionsListException
    {
        public ExistsOptionsListCodeException(string code)
            : base(string.Format(ExistsOptionsListCodeMessage, code)) { }
    }
}