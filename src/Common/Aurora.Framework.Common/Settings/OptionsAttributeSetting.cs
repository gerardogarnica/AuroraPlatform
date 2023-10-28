using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    public class OptionsAttributeSetting : AttributeSettingBase
    {
        public string Code { get; set; }
        public bool AllowMultipleValues { get; set; }
        public int MaxSelectedItems { get; set; }
        public IList<string> DefaultItemCodes { get; set; }
        public bool ShowInactiveItems { get; set; }

        public OptionsAttributeSetting()
            : base(null) { }

        public OptionsAttributeSetting(string xmlSetting)
            : base(xmlSetting)
        {
            var q = from b in XDocument.Descendants("catalogSetting")
                    select new
                    {
                        Code = (string)b.Element("code"),
                        AllowMultipleValues = (bool)b.Element("allowMultipleValues"),
                        MaxSelectedItem = (int)b.Element("maxSelectedItem"),
                        DefaultItemCodes = (string)b.Element("defaultItemCodes"),
                        ShowInactiveItems = (bool)b.Element("showInactiveItems")
                    };

            Code = q.FirstOrDefault().Code;
            AllowMultipleValues = q.FirstOrDefault().AllowMultipleValues;
            MaxSelectedItems = q.FirstOrDefault().MaxSelectedItem;
            DefaultItemCodes = q.FirstOrDefault().DefaultItemCodes.Split(";").ToList();
            ShowInactiveItems = q.FirstOrDefault().ShowInactiveItems;
        }

        public override string GetSettingWrapper()
        {
            var document = new XDocument(
                new XElement("catalogSetting",
                    new XElement("code", Code),
                    new XElement("allowMultipleValues", AllowMultipleValues),
                    new XElement("maxSelectedItem", MaxSelectedItems),
                    new XElement("defaultItemCodes", string.Join(";", DefaultItemCodes.ToArray())),
                    new XElement("showInactiveItems", ShowInactiveItems)));

            return document.ToString();
        }
    }
}