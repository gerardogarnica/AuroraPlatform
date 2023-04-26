using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class MoneyAttributeSetting : AttributeSettingBase
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal DefaultValue { get; set; }

        public MoneyAttributeSetting()
            : base(null) { }

        public MoneyAttributeSetting(string xmlSetting)
            : base(xmlSetting)
        {
            var q = from b in XDocument.Descendants("moneySetting")
                    select new
                    {
                        MinValue = (decimal)b.Element("minValue"),
                        MaxValue = (decimal)b.Element("maxValue"),
                        DefaultValue = (decimal)b.Element("defaultValue")
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
                new XElement("moneySetting",
                    new XElement("minValue", MinValue.ToCurrency()),
                    new XElement("maxValue", MaxValue.ToCurrency()),
                    new XElement("defaultValue", DefaultValue.ToCurrency())));

            return document.ToString();
        }
    }
}