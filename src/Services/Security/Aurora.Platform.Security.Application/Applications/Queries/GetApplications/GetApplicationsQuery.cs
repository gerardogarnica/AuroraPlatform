using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;
using ApplicationEntity = Aurora.Platform.Security.Domain.Entities.Application;

namespace Aurora.Platform.Security.Application.Applications.Queries.GetApplications;

public record GetApplicationsQuery : IRequest<IReadOnlyList<ApplicationEntity>> { }

public class GetApplicationsHandler : IRequestHandler<GetApplicationsQuery, IReadOnlyList<ApplicationEntity>>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IApplicationRepository _applicationRepository;

    #endregion

    #region Constructor

    public GetApplicationsHandler(
        IMapper mapper,
        IApplicationRepository applicationRepository)
    {
        _mapper = mapper;
        _applicationRepository = applicationRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<IReadOnlyList<ApplicationEntity>> IRequestHandler<GetApplicationsQuery, IReadOnlyList<ApplicationEntity>>.Handle(
        GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = await _applicationRepository.GetListAsync(x => x.Id == x.Id, x => x.Name);

        return _mapper.Map<IReadOnlyList<ApplicationEntity>>(applications);
    }

    #endregion
}