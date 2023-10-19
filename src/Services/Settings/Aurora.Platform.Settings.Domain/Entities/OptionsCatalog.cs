using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class OptionsCatalog : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGlobal { get; set; }
        public string AppCode { get; set; }
        public string AppName { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public List<OptionsCatalogItem> Items { get; set; }
    }
}