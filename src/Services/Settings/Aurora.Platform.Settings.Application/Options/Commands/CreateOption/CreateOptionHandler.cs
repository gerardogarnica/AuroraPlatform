using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Exceptions;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;
using OptionsCatalogModel = Aurora.Framework.Platform.Options.OptionsCatalog;

namespace Aurora.Platform.Settings.Application.Options.Commands.CreateOption;

public class CreateOptionHandler : IRequestHandler<CreateOptionCommand, OptionsCatalogModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IOptionsCatalogRepository _optionsRepository;

    #endregion

    #region Constructor

    public CreateOptionHandler(
        IMapper mapper,
        IOptionsCatalogRepository optionsRepository)
    {
        _mapper = mapper;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<OptionsCatalogModel> IRequestHandler<CreateOptionCommand, OptionsCatalogModel>.Handle(
        CreateOptionCommand request, CancellationToken cancellationToken)
    {
        // Do business validations
        await CheckIfCodeIsAvailable(request.Code);

        // Create option entity
        var option = _mapper.Map<OptionsCatalog>(request);

        // Add option entity to repository
        option = await _optionsRepository.AddAsync(option);

        // Returns option model
        return _mapper.Map<OptionsCatalogModel>(option);
    }

    #endregion

    #region Private methods

    private async Task CheckIfCodeIsAvailable(string code)
    {
        var option = await _optionsRepository.GetByCodeAsync(code);
        if (option != null)
            throw new OptionCodeAlreadyExistsException(code);
    }

    #endregion
}