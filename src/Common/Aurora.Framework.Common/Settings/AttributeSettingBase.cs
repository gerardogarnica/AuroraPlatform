using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public abstract class AttributeSettingBase
    {
        public XDocument XDocument;

        public AttributeSettingBase(string xmlSetting)
        {
            if (!string.IsNullOrWhiteSpace(xmlSetting))
                XDocument = XDocument.Parse(xmlSetting);
        }

        public abstract string GetSettingWrapper();

        protected void ThrowException(string message)
        {
            throw new BusinessException("AttributeSetting", message);
        }
    }
}