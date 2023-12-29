using Aurora.Framework.Settings;
using MediatR;
using AttributeValueModel = Aurora.Framework.Platform.Attributes.AttributeValue;

namespace Aurora.Platform.Settings.Application.Attributes.Commands.SaveValue;

public class SaveValueCommand : AuroraAttributeValue, IRequest<AttributeValueModel>
{
    public string Code { get; init; }
    public int RelationshipId { get; init; }
    public string Notes { get; init; }
}