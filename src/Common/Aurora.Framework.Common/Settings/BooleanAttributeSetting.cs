using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class BooleanAttributeSetting : AttributeSettingBase
    {
        public bool DefaultValue { get; set; }

        public BooleanAttributeSetting()
            : base(null) { }

        public BooleanAttributeSetting(string xmlSetting)
            : base(xmlSetting)
        {
            var q = from b in XDocument.Descendants("booleanSetting")
                    select new
                    {
                        DefaultValue = (bool?)b.Element("defaultValue")
                    };

            DefaultValue = q.FirstOrDefault().DefaultValue.Value;
        }

        public override string GetSettingWrapper()
        {
            var document = new XDocument(
                new XElement("booleanSetting",
                    new XElement("defaultValue", DefaultValue)));

            return document.ToString();
        }
    }
}