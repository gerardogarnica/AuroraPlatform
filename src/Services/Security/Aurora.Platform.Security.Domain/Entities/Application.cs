using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class Application : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasCustomConfig { get; set; }
    }
}