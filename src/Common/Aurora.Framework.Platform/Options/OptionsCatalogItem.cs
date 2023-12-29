namespace Aurora.Framework.Platform.Options
{
    public class OptionsCatalogItem
    {
        public int ItemId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
    }
}