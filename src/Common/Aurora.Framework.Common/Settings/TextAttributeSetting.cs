using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class TextAttributeSetting : AttributeSettingBase
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string DefaultValue { get; set; }
        public string Pattern { get; set; }

        public TextAttributeSetting()
            : base(null) { }

        public TextAttributeSetting(string xmlSetting)
            : base(xmlSetting)
        {
            var q = from b in XDocument.Descendants("textSetting")
                    select new
                    {
                        MinLength = (int)b.Element("minLength"),
                        MaxLength = (int)b.Element("maxLength"),
                        DefaultValue = (string)b.Element("defaultValue"),
                        Pattern = (string)b.Element("pattern")
                    };

            MinLength = q.FirstOrDefault().MinLength;
            MaxLength = q.FirstOrDefault().MaxLength;
            DefaultValue = q.FirstOrDefault().DefaultValue;
            Pattern = q.FirstOrDefault().Pattern;
        }

        public override string GetSettingWrapper()
        {
            if (MaxLength < MinLength)
            {
                ThrowException(string.Format(ExceptionMessages.InvalidLengthRangeValueAttributeSetting, MinLength, MaxLength));
            }

            if (!string.IsNullOrWhiteSpace(DefaultValue))
            {
                if (!DefaultValue.Length.IsIntoInterval(MinLength, MaxLength))
                {
                    ThrowException(string.Format(ExceptionMessages.InvalidLengthDefaultValueAttributeSetting, DefaultValue, MinLength, MaxLength));
                }

                if (!string.IsNullOrWhiteSpace(Pattern))
                {
                    if (Regex.IsMatch(DefaultValue, Pattern))
                        ThrowException(string.Format(ExceptionMessages.InvalidPatternValueAttributeSetting, DefaultValue, Pattern));
                }
            }

            var document = new XDocument(
                new XElement("textSetting",
                    new XElement("minLength", MinLength),
                    new XElement("maxLength", MaxLength),
                    new XElement("defaultValue", DefaultValue),
                    new XElement("pattern", Pattern)));

            return document.ToString();
        }
    }
}