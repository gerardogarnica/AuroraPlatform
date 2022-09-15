using Aurora.Platform.Settings.Application.Queries;
using MediatR;

namespace Aurora.Platform.Settings.Application.Commands
{
    public class CreateOptionsListCommand : IRequest<OptionsListViewModel>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public IList<CreateOptionsListItem>? Items { get; set; } = new List<CreateOptionsListItem>();
    }

    public class CreateOptionsListItem
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
    }
}