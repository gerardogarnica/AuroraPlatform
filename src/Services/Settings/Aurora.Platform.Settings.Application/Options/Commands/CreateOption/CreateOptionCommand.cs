using MediatR;

namespace Aurora.Platform.Settings.Application.Options.Commands.CreateOption;

public record CreateOptionCommand : IRequest<OptionsCatalogModel>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsGlobal { get; set; }
    public string AppCode { get; set; }
    public string AppName { get; set; }
    public bool IsVisible { get; set; }
    public bool IsEditable { get; set; }
    public List<CreateOptionItem> Items { get; set; }
}

public record CreateOptionItem
{
    public string Code { get; set; }
    public string Description { get; set; }
    public bool IsEditable { get; set; }
    public bool IsActive { get; set; }
}