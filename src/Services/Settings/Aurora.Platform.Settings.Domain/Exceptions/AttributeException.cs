using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class AttributeException : BusinessException
    {
        protected const string AttributeNullMessage = "The record of the attribute cannot be null.";
        protected const string InvalidSettingCodeMessage = "The code '{0}' does not exist.";

        public AttributeException(string message)
            : base("AttributeException", message) { }
    }

    public class AttributeNullException : AttributeException
    {
        public AttributeNullException()
            : base(AttributeNullMessage) { }
    }

    public class InvalidSettingCodeException : AttributeException
    {
        public InvalidSettingCodeException(string code)
            : base(string.Format(InvalidSettingCodeMessage, code)) { }
    }
}