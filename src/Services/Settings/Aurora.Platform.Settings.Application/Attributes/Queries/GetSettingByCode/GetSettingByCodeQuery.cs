using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Settings.Application.Attributes.Queries.GetSettingByCode;

public record GetSettingByCodeQuery : IRequest<AttributeSettingModel>
{
    public string Code { get; init; }
}

public class GetSettingByCodeHandler : IRequestHandler<GetSettingByCodeQuery, AttributeSettingModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IAttributeSettingRepository _settingRepository;

    #endregion

    #region Constructor

    public GetSettingByCodeHandler(
        IMapper mapper,
        IAttributeSettingRepository settingRepository)
    {
        _mapper = mapper;
        _settingRepository = settingRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<AttributeSettingModel> IRequestHandler<GetSettingByCodeQuery, AttributeSettingModel>.Handle(
        GetSettingByCodeQuery request, CancellationToken cancellationToken)
    {
        // Get setting
        var setting = await _settingRepository.GetAsync(x => x.Code.Equals(request.Code));
        if (setting == null) return null;

        // Returns setting model
        return _mapper.Map<AttributeSettingModel>(setting);
    }

    #endregion
}