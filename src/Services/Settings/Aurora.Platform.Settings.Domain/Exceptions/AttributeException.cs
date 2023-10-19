using Aurora.Framework;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class AttributeException : BusinessException
    {
        public AttributeException(string message)
            : base("AttributeException", message) { }
    }

    public class AttributeNullException : AttributeException
    {
        public AttributeNullException()
            : base("The record of the attribute cannot be null.") { }
    }

    public class InvalidSettingCodeException : AttributeException
    {
        public InvalidSettingCodeException(string code)
            : base($"The code '{code}' does not exist.") { }
    }
}