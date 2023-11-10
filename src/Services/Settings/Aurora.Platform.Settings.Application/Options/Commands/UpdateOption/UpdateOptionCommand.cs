using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Exceptions;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Settings.Application.Options.Commands.UpdateOption;

public record UpdateOptionCommand : IRequest<OptionsCatalogModel>
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool IsVisible { get; init; }
    public bool IsEditable { get; init; }
}

public class UpdateOptionHandler : IRequestHandler<UpdateOptionCommand, OptionsCatalogModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IOptionsCatalogRepository _optionsRepository;

    #endregion

    #region Constructor

    public UpdateOptionHandler(
        IMapper mapper,
        IOptionsCatalogRepository optionsRepository)
    {
        _mapper = mapper;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<OptionsCatalogModel> IRequestHandler<UpdateOptionCommand, OptionsCatalogModel>.Handle(
        UpdateOptionCommand request, CancellationToken cancellationToken)
    {
        // Get option
        var option = await GetOptionAsync(request.Code);

        // Update option entity in repository
        option.Name = request.Name.Trim();
        option.Description = request.Description.Trim();
        option.IsVisible = request.IsVisible;
        option.IsEditable = request.IsEditable;

        option = await _optionsRepository.UpdateAsync(option);

        // Returns option model
        return _mapper.Map<OptionsCatalogModel>(option);
    }

    #endregion

    #region Private methods

    private async Task<OptionsCatalog> GetOptionAsync(string code)
    {
        var option = await
            _optionsRepository.GetByCodeAsync(code)
            ?? throw new InvalidOptionCodeException(code);

        option.CheckIfIsEditable();

        return option;
    }

    #endregion
}