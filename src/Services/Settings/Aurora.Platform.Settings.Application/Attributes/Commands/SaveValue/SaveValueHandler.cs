using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Exceptions;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Settings.Application.Attributes.Commands.SaveValue;

public class SaveValueHandler : IRequestHandler<SaveValueCommand, AttributeValueModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IAttributeSettingRepository _settingRepository;
    private readonly IAttributeValueRepository _valueRepository;

    #endregion

    #region Constructor

    public SaveValueHandler(
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

    async Task<AttributeValueModel> IRequestHandler<SaveValueCommand, AttributeValueModel>.Handle(
        SaveValueCommand request, CancellationToken cancellationToken)
    {
        // Get setting model
        var settingModel = await GetSettingModel(request.Code);

        // Get value
        var value = await _valueRepository.GetByCodeAsync(request.Code, request.RelationshipId);

        if (value == null)
        {
            // Create value entity
            value = new AttributeValue()
            {
                Id = settingModel.AttributeId,
                RelationshipId = request.RelationshipId,
                Value = request.GetValueString(settingModel),
                Notes = request.Notes
            };

            // Add value repository
            await _valueRepository.AddAsync(value);

            // Get new value
            value = await _valueRepository.GetByCodeAsync(request.Code, request.RelationshipId);
        }
        else
        {
            // Update value entity
            value.Value = request.GetValueString(settingModel);
            value.Notes = request.Notes.Trim();

            // Update value repository
            value = await _valueRepository.UpdateAsync(value);
        }

        // Returns value model
        return _mapper.Map<AttributeValueModel>(value);
    }

    #endregion

    #region Private methods

    private async Task<AttributeSettingModel> GetSettingModel(string code)
    {
        var setting = await _settingRepository.GetAsync(x => x.Code.Equals(code)) ?? throw new InvalidSettingCodeException(code);

        return _mapper.Map<AttributeSettingModel>(setting);
    }

    #endregion
}