using Aurora.Framework.Settings;
using MediatR;

namespace Aurora.Platform.Settings.Application.Attributes.Commands.CreateSetting;

public class CreateSettingCommand : AuroraAttributeSetting, IRequest<AttributeSettingModel>
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string ScopeType { get; init; }
    public bool IsVisible { get; init; }
    public bool IsEditable { get; init; }
    public bool IsActive { get; init; }
}