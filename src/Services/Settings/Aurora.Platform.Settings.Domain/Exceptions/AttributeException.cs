using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Settings.Domain.Exceptions
{
    public class AttributeException : BusinessException
    {
        protected const string AttributeNullMessage = "The record of the attribute cannot be null.";

        public AttributeException(string message)
            : base("AttributeException", message) { }
    }

    public class AttributeNullException : AttributeException
    {
        public AttributeNullException()
            : base(AttributeNullMessage) { }
    }
}