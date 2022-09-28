using Aurora.Framework.Exceptions;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class IntegerAttributeValue : AttributeValueBase
    {
        public int Value { get; set; }

        public IntegerAttributeValue()
            : base(null) { }

        public IntegerAttributeValue(string xmlValue)
            : base(xmlValue)
        {
            var q = from b in XDocument.Descendants("integerValue")
                    select new
                    {
                        Value = (int)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        public string GetValueWrapper(IntegerAttributeSetting setting)
        {
            if (!Value.IsIntoInterval(setting.MinValue, setting.MaxValue))
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidRangeAttributeValue, Value, setting.MinValue, setting.MaxValue));
            }

            var document = new XDocument(
                new XElement("integerValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}