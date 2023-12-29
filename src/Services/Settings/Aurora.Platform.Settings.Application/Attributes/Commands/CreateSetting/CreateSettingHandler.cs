using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Exceptions;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;
using AttributeSettingModel = Aurora.Framework.Platform.Attributes.AttributeSetting;

namespace Aurora.Platform.Settings.Application.Attributes.Commands.CreateSetting;

public class CreateSettingHandler : IRequestHandler<CreateSettingCommand, AttributeSettingModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IAttributeSettingRepository _settingRepository;
    private readonly IOptionsCatalogRepository _optionsRepository;

    #endregion

    #region Constructor

    public CreateSettingHandler(
        IMapper mapper,
        IAttributeSettingRepository settingRepository,
        IOptionsCatalogRepository optionsRepository)
    {
        _mapper = mapper;
        _settingRepository = settingRepository;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<AttributeSettingModel> IRequestHandler<CreateSettingCommand, AttributeSettingModel>.Handle(
        CreateSettingCommand request, CancellationToken cancellationToken)
    {
        // Do business validations
        await CheckIfCodeIsAvailable(request.Code, request.ScopeType);
        await CheckIfScopeExists(request.ScopeType);

        // Create setting entity
        var setting = _mapper.Map<AttributeSetting>(request);

        // Add setting entity to repository
        setting = await _settingRepository.AddAsync(setting);

        // Returns setting model
        return _mapper.Map<AttributeSettingModel>(setting);
    }

    #endregion

    #region Private methods

    private async Task CheckIfCodeIsAvailable(string code, string scopeType)
    {
        var setting = await _settingRepository.GetAsync(x => x.Code.Equals(code) && x.ScopeType.Equals(scopeType));
        if (setting != null)
            throw new SettingCodeAlreadyExistsException(code, scopeType);
    }

    private async Task CheckIfScopeExists(string scopeType)
    {
        var optionCode = "AttributeType";

        var option = await
            _optionsRepository.GetByCodeAsync(optionCode)
            ?? throw new InvalidOptionCodeException(optionCode);

        if (!option.Items.Any(x => x.Code.Equals(scopeType)))
            throw new InvalidOptionItemCodeException(optionCode, scopeType);
    }

    #endregion
}