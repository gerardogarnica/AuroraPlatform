using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class TextAttributeValue : AttributeValueBase
    {
        public string Value { get; set; }

        public TextAttributeValue()
            : base(null) { }

        public TextAttributeValue(string xmlValue)
            : base(xmlValue)
        {
            var q = from b in XDocument.Descendants("textValue")
                    select new
                    {
                        Value = (string)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        public string GetValueWrapper(TextAttributeSetting setting)
        {
            if (Value != null)
            {
                if (!Value.Length.IsIntoInterval(setting.MinLength, setting.MaxLength))
                {
                    ThrowException(string.Format(ExceptionMessages.InvalidLengthAttributeValue, Value, setting.MinLength, setting.MaxLength));
                }

                if (!string.IsNullOrWhiteSpace(setting.Pattern))
                {
                    if (Regex.IsMatch(Value, setting.Pattern))
                        ThrowException(string.Format(ExceptionMessages.InvalidPatternAttributeValue, Value, setting.Pattern));
                }
            }

            var document = new XDocument(
                new XElement("textValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}