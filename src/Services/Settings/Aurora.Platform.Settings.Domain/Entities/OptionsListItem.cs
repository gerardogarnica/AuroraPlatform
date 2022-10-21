using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class OptionsListItem : AuditableEntity
    {
        public int OptionsId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public OptionsList List { get; set; } = new OptionsList();
    }
}