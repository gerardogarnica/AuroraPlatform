using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;
using AttributeSettingModel = Aurora.Framework.Platform.Attributes.AttributeSetting;
using AttributeValueModel = Aurora.Framework.Platform.Attributes.AttributeValue;

namespace Aurora.Platform.Settings.Application.Attributes.Queries.GetValues;

public record GetValuesQuery : IRequest<IReadOnlyList<AttributeValueModel>>
{
    public string ScopeType { get; init; }
    public int RelationshipId { get; init; }
}

public class GetValuesHandler : IRequestHandler<GetValuesQuery, IReadOnlyList<AttributeValueModel>>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IAttributeSettingRepository _settingRepository;
    private readonly IAttributeValueRepository _valueRepository;

    #endregion

    #region Constructor

    public GetValuesHandler(
        IMapper mapper,
        IAttributeSettingRepository settingRepository,
        IAttributeValueRepository valueRepository)
    {
        _mapper = mapper;
        _settingRepository = settingRepository;
        _valueRepository = valueRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<IReadOnlyList<AttributeValueModel>> IRequestHandler<GetValuesQuery, IReadOnlyList<AttributeValueModel>>.Handle(
        GetValuesQuery request, CancellationToken cancellationToken)
    {
        // Get values
        var list = await _valueRepository.GetListAsync(request.ScopeType, request.RelationshipId);
        var values = list.ToList();

        // Get settings
        var settings = await _settingRepository.GetListAsync(x => x.ScopeType.Equals(request.ScopeType), x => x.Name);

        foreach (var setting in settings)
        {
            if (values.Any(x => x.Id.Equals(setting.Id))) continue;

            // Map to setting model
            var settingModel = _mapper.Map<AttributeSettingModel>(setting);
            if (settingModel == null) continue;

            // Get default value
            values.Add(setting.GetDefaultAttributeValue(settingModel, request.RelationshipId));
        }

        // Returns values list model
        return _mapper.Map<List<AttributeValueModel>>(values);
    }

    #endregion
}