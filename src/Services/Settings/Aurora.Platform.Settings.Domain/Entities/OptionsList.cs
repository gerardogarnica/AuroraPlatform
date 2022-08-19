using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class OptionsList : EntityBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public IList<OptionsListItem>? Items { get; set; }
    }
}