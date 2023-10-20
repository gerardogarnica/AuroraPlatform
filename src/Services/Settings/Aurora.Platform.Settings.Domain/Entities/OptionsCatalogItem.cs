using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class OptionsCatalogItem : AuditableEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int OptionsId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public OptionsCatalog Catalog { get; set; }
    }
}