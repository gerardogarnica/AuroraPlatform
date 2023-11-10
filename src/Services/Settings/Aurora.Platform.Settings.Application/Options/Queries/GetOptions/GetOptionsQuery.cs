using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace Aurora.Platform.Settings.Application.Options.Queries.GetOptions;

public record GetOptionsQuery : IRequest<PagedCollection<OptionsCatalogModel>>
{
    public PagedViewRequest PagedViewRequest { get; init; }
    public string Search { get; init; }
    public bool OnlyVisibles { get; init; }
}

public class GetOptionsHandler : IRequestHandler<GetOptionsQuery, PagedCollection<OptionsCatalogModel>>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IOptionsCatalogRepository _optionsRepository;

    #endregion

    #region Constructor

    public GetOptionsHandler(
        IMapper mapper,
        IOptionsCatalogRepository optionsRepository)
    {
        _mapper = mapper;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<PagedCollection<OptionsCatalogModel>> IRequestHandler<GetOptionsQuery, PagedCollection<OptionsCatalogModel>>.Handle(
        GetOptionsQuery request, CancellationToken cancellationToken)
    {
        // Get predicate
        Expression<Func<OptionsCatalog, bool>> predicate = x => x.Id == x.Id;

        if (!string.IsNullOrWhiteSpace(request.Search) && request.Search.Length >= 3)
            predicate = predicate.And(x => x.Code.Contains(request.Search) || x.Name.Contains(request.Search) || x.Description.Contains(request.Search));
        if (request.OnlyVisibles)
            predicate = predicate.And(x => x.IsVisible);

        // Get paged options
        var options = await _optionsRepository
            .GetPagedListAsync(request.PagedViewRequest, predicate, x => x.Name);

        // Returns paged options model
        return _mapper.Map<PagedCollection<OptionsCatalogModel>>(options);
    }

    #endregion
}