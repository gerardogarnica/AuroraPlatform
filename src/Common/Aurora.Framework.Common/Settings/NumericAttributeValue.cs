using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class NumericAttributeValue : AttributeValueBase
    {
        public decimal Value { get; set; }

        public NumericAttributeValue()
            : base(null) { }

        public NumericAttributeValue(string xmlValue)
            : base(xmlValue)
        {
            var q = from b in XDocument.Descendants("numericValue")
                    select new
                    {
                        Value = (decimal)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        public string GetValueWrapper(NumericAttributeSetting setting)
        {
            if (!Value.IsIntoInterval(setting.MinValue, setting.MaxValue))
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidRangeAttributeValue, Value, setting.MinValue, setting.MaxValue));
            }

            var document = new XDocument(
                new XElement("numericValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}