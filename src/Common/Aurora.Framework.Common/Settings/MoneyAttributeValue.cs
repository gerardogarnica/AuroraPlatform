using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class MoneyAttributeValue : AttributeValueBase
    {
        public decimal Value { get; set; }

        public MoneyAttributeValue()
            : base(null) { }

        public MoneyAttributeValue(string xmlValue)
            : base(xmlValue)
        {
            var q = from b in XDocument.Descendants("moneyValue")
                    select new
                    {
                        Value = (decimal)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        public string GetValueWrapper(MoneyAttributeSetting setting)
        {
            if (!Value.IsIntoInterval(setting.MinValue, setting.MaxValue))
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidRangeAttributeValue, Value, setting.MinValue, setting.MaxValue));
            }

            var document = new XDocument(
                new XElement("moneyValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}