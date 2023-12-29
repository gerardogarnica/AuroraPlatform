using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using AttributeSettingModel = Aurora.Framework.Platform.Attributes.AttributeSetting;

namespace Aurora.Platform.Settings.Application.Attributes.Queries.GetSettings;

public record GetSettingsQuery : IRequest<PagedCollection<AttributeSettingModel>>
{
    public PagedViewRequest PagedViewRequest { get; init; }
    public string Scope { get; init; }
}

public class GetSettingsHandler : IRequestHandler<GetSettingsQuery, PagedCollection<AttributeSettingModel>>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IAttributeSettingRepository _settingRepository;

    #endregion

    #region Constructor

    public GetSettingsHandler(
        IMapper mapper,
        IAttributeSettingRepository settingRepository)
    {
        _mapper = mapper;
        _settingRepository = settingRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<PagedCollection<AttributeSettingModel>> IRequestHandler<GetSettingsQuery, PagedCollection<AttributeSettingModel>>.Handle(
        GetSettingsQuery request, CancellationToken cancellationToken)
    {
        // Add filters
        Expression<Func<AttributeSetting, bool>> predicate = x => x.Equals(x);
        if (!string.IsNullOrWhiteSpace(request.Scope))
            predicate = predicate.And(x => x.ScopeType.Equals(request.Scope));

        // Get settings
        var settings = await _settingRepository
            .GetPagedListAsync(request.PagedViewRequest, predicate, x => x.Name);

        // Returns settings model list
        return _mapper.Map<PagedCollection<AttributeSettingModel>>(settings);
    }

    #endregion
}