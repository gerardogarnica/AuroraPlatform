using Aurora.Framework.Exceptions;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class OptionsListAttributeValue : AttributeValueBase
    {
        public IList<string> ItemCodes { get; set; }

        public OptionsListAttributeValue()
            : base(null) { }

        public OptionsListAttributeValue(string xmlValue)
            : base(xmlValue)
        {
            var q = from b in XDocument.Descendants("catalogValue")
                    select new
                    {
                        ItemCodes = (string)b.Element("itemCodes")
                    };

            ItemCodes = q.FirstOrDefault().ItemCodes.Split(";").ToList();
        }

        public string GetValueWrapper(OptionsListAttributeSetting setting)
        {
            if (setting.AllowMultipleValues && ItemCodes.Count > setting.MaxSelectedItems)
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidOptionsListItemAttributeValue, setting.MaxSelectedItems));
            }

            var codes = string.Join(";", ItemCodes.ToArray());

            var document = new XDocument(
                new XElement("catalogValue",
                    new XElement("itemCodes", codes)));

            return document.ToString();
        }
    }
}