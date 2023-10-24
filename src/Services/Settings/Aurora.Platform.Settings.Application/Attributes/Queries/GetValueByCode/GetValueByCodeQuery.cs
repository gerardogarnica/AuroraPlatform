using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Settings.Application.Attributes.Queries.GetValueByCode;

public record GetValueByCodeQuery : IRequest<AttributeValueModel>
{
    public string Code { get; init; }
    public int RelationshipId { get; init; }
}

public class GetValueByCodeHandler : IRequestHandler<GetValueByCodeQuery, AttributeValueModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IAttributeSettingRepository _settingRepository;
    private readonly IAttributeValueRepository _valueRepository;

    #endregion

    #region Constructor

    public GetValueByCodeHandler(
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

    async Task<AttributeValueModel> IRequestHandler<GetValueByCodeQuery, AttributeValueModel>.Handle(
        GetValueByCodeQuery request, CancellationToken cancellationToken)
    {
        // Get value
        var value = await _valueRepository.GetByCodeAsync(request.Code, request.RelationshipId);

        if (value == null)
        {
            // Get setting
            var setting = await _settingRepository.GetAsync(x => x.Code.Equals(request.Code));
            if (setting == null) return null;

            // Map to setting model
            var settingModel = _mapper.Map<AttributeSettingModel>(setting);
            if (settingModel == null) return null;

            // Get default value
            value = settingModel.GetDefaultAttributeValue(setting, request.RelationshipId);
        }

        // Returns value model
        return _mapper.Map<AttributeValueModel>(value);
    }

    #endregion
}