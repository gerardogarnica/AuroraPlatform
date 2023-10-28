using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class OptionsCatalogAttributeValue : AttributeValueBase
    {
        public IList<string> ItemCodes { get; set; }

        public OptionsCatalogAttributeValue()
            : base(null) { }

        public OptionsCatalogAttributeValue(string xmlValue)
            : base(xmlValue)
        {
            var q = from b in XDocument.Descendants("catalogValue")
                    select new
                    {
                        ItemCodes = (string)b.Element("itemCodes")
                    };

            ItemCodes = q.FirstOrDefault().ItemCodes.Split(";").ToList();
        }

        public string GetValueWrapper(OptionsCatalogAttributeSetting setting)
        {
            if (setting.AllowMultipleValues && ItemCodes.Count > setting.MaxSelectedItems)
            {
                ThrowException(string.Format(ExceptionMessages.InvalidOptionsCatalogItemAttributeValue, setting.MaxSelectedItems));
            }

            var codes = string.Join(";", ItemCodes.ToArray());

            var document = new XDocument(
                new XElement("catalogValue",
                    new XElement("itemCodes", codes)));

            return document.ToString();
        }
    }
}