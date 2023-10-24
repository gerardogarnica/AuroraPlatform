using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Settings.Application.Options.Queries.GetOptionByCode;

public record GetOptionByCodeQuery : IRequest<OptionsCatalogModel>
{
    public string Code { get; init; }
    public bool OnlyActiveItems { get; init; }
}

public class GetOptionByCodeHandler : IRequestHandler<GetOptionByCodeQuery, OptionsCatalogModel>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IOptionsCatalogRepository _optionsRepository;

    #endregion

    #region Constructor

    public GetOptionByCodeHandler(
        IMapper mapper,
        IOptionsCatalogRepository optionsRepository)
    {
        _mapper = mapper;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<OptionsCatalogModel> IRequestHandler<GetOptionByCodeQuery, OptionsCatalogModel>.Handle(
        GetOptionByCodeQuery request, CancellationToken cancellationToken)
    {
        // Get option
        var option = await _optionsRepository.GetByCodeAsync(request.Code);
        if (option == null) return null;

        if (request.OnlyActiveItems)
        {
            option.Items.ToList().RemoveAll(x => x.IsActive == false);
        }

        // Returns option model
        return _mapper.Map<OptionsCatalogModel>(option);
    }

    #endregion
}