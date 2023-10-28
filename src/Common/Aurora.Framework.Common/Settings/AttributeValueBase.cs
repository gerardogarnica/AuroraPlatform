using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public abstract class AttributeValueBase
    {
        public XDocument XDocument;

        public AttributeValueBase(string xmlValue)
        {
            if (!string.IsNullOrWhiteSpace(xmlValue))
                XDocument = XDocument.Parse(xmlValue);
        }

        protected void ThrowException(string message)
        {
            throw new BusinessException("AttributeValue", message);
        }
    }
}