using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public abstract class AttributeSettingBase
    {
        public XDocument XDocument;

        public AttributeSettingBase(string xmlSetting)
        {
            XDocument = XDocument.Parse(xmlSetting);
        }

        public abstract string GetConfigurationWrapper();
    }
}