using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Exceptions;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Settings.Application.Options.Commands.SaveItem;

public record SaveItemCommand : IRequest<OptionsCatalogModel>
{
    public string OptionsCode { get; set; }
    public string ItemCode { get; set; }
    public string Description { get; set; }
    public bool IsEditable { get; set; }
    public bool IsActive { get; set; }
}

public class SaveItemHandler : IRequestHandler<SaveItemCommand, OptionsCatalogModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IOptionsCatalogRepository _optionsRepository;

    #endregion

    #region Constructor

    public SaveItemHandler(
        IMapper mapper,
        IOptionsCatalogRepository optionsRepository)
    {
        _mapper = mapper;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<OptionsCatalogModel> IRequestHandler<SaveItemCommand, OptionsCatalogModel>.Handle(
        SaveItemCommand request, CancellationToken cancellationToken)
    {
        // Get option
        var option = await GetOptionAsync(request.OptionsCode);

        // Get option item
        var item = option.Items.FirstOrDefault(x => x.Code == request.ItemCode);

        if (item == null)
        {
            // Create option item entity
            item = _mapper.Map<Domain.Entities.OptionsCatalogItem>(request);

            // Add item to option
            option.Items.Add(item);
        }
        else
        {
            // Update option item entity
            item.Description = request.Description.Trim();
            item.IsEditable = request.IsEditable;
            item.IsActive = request.IsActive;
        }

        // Update option entity in repository
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

        return option;
    }

    #endregion
}