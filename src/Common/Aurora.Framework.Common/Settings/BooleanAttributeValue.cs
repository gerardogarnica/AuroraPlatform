using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class BooleanAttributeValue : AttributeValueBase
    {
        public bool Value { get; set; }

        public BooleanAttributeValue()
            : base(null) { }

        public BooleanAttributeValue(string xmlValue)
            : base(xmlValue)
        {
            var q = from b in XDocument.Descendants("booleanValue")
                    select new
                    {
                        Value = (bool)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        public string GetValueWrapper()
        {
            var document = new XDocument(
                new XElement("booleanValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}