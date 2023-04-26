using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class IntegerAttributeSetting : AttributeSettingBase
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int DefaultValue { get; set; }

        public IntegerAttributeSetting()
            : base(null) { }

        public IntegerAttributeSetting(string xmlSetting)
            : base(xmlSetting)
        {
            var q = from b in XDocument.Descendants("integerSetting")
                    select new
                    {
                        MinValue = (int)b.Element("minValue"),
                        MaxValue = (int)b.Element("maxValue"),
                        DefaultValue = (int)b.Element("defaultValue")
                    };

            MinValue = q.FirstOrDefault().MinValue;
            MaxValue = q.FirstOrDefault().MaxValue;
            DefaultValue = q.FirstOrDefault().DefaultValue;
        }

        public override string GetSettingWrapper()
        {
            if (MaxValue < MinValue)
            {
                throw new PlatformException(string.Format(ExceptionMessages.InvalidRangeValueAttributeSetting, MinValue, MaxValue));
            }

            if (!DefaultValue.IsIntoInterval(MinValue, MaxValue))
            {
                throw new PlatformException(string.Format(ExceptionMessages.InvalidDefaultValueAttributeSetting, DefaultValue, MinValue, MaxValue));
            }

            var document = new XDocument(
                new XElement("integerSetting",
                    new XElement("minValue", MinValue),
                    new XElement("maxValue", MaxValue),
                    new XElement("defaultValue", DefaultValue)));

            return document.ToString();
        }
    }
}