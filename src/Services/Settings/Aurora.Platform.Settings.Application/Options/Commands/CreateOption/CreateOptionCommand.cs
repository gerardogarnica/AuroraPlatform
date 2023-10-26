using MediatR;

namespace Aurora.Platform.Settings.Application.Options.Commands.CreateOption;

public record CreateOptionCommand : IRequest<OptionsCatalogModel>
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool IsGlobal { get; init; }
    public string AppCode { get; init; }
    public string AppName { get; init; }
    public bool IsVisible { get; init; }
    public bool IsEditable { get; init; }
    public List<CreateOptionItem> Items { get; init; }
}

public record CreateOptionItem
{
    public string Code { get; init; }
    public string Description { get; init; }
    public bool IsEditable { get; init; }
    public bool IsActive { get; init; }
}