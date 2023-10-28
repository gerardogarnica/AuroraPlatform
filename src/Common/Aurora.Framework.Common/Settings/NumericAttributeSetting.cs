using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class NumericAttributeSetting : AttributeSettingBase
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal DefaultValue { get; set; }
        public int DecimalsQuantity { get; set; }

        public NumericAttributeSetting()
            : base(null) { }

        public NumericAttributeSetting(string xmlSetting)
            : base(xmlSetting)
        {
            var q = from b in XDocument.Descendants("numericSetting")
                    select new
                    {
                        MinValue = (decimal)b.Element("minValue"),
                        MaxValue = (decimal)b.Element("maxValue"),
                        DefaultValue = (decimal)b.Element("defaultValue"),
                        DecimalsQuantity = (int)b.Element("decimalsQuantity")
                    };

            MinValue = q.FirstOrDefault().MinValue;
            MaxValue = q.FirstOrDefault().MaxValue;
            DefaultValue = q.FirstOrDefault().DefaultValue;
            DecimalsQuantity = q.FirstOrDefault().DecimalsQuantity;
        }

        public override string GetSettingWrapper()
        {
            if (MaxValue < MinValue)
            {
                ThrowException(string.Format(ExceptionMessages.InvalidRangeValueAttributeSetting, MinValue, MaxValue));
            }

            if (!DefaultValue.IsIntoInterval(MinValue, MaxValue))
            {
                ThrowException(string.Format(ExceptionMessages.InvalidDefaultValueAttributeSetting, DefaultValue, MinValue, MaxValue));
            }

            var document = new XDocument(
                new XElement("numericSetting",
                    new XElement("minValue", MinValue.Round(DecimalsQuantity)),
                    new XElement("maxValue", MaxValue.Round(DecimalsQuantity)),
                    new XElement("defaultValue", DefaultValue.Round(DecimalsQuantity)),
                    new XElement("decimalsQuantity", DecimalsQuantity)));

            return document.ToString();
        }
    }
}