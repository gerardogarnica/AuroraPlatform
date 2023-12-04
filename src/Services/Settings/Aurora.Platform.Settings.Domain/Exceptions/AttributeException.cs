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

    public class SettingCodeAlreadyExistsException : AttributeException
    {
        public SettingCodeAlreadyExistsException(string code, string scopeType)
            : base($"The attribute setting code '{code}' already exists in '{scopeType}' scope and cannot be created again.") { }
    }

    public class InvalidSettingCodeException : AttributeException
    {
        public InvalidSettingCodeException(string code)
            : base($"The attribute setting code '{code}' does not exist.") { }
    }
}