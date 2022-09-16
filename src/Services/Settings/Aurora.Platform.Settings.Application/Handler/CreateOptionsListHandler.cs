using Aurora.Platform.Settings.Application.Commands;
using Aurora.Platform.Settings.Application.Queries;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Settings.Application.Handler
{
    public class CreateOptionsListHandler : IRequestHandler<CreateOptionsListCommand, OptionsListViewModel>
    {
        #region Private members

        private readonly IOptionsListRepository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public CreateOptionsListHandler(IOptionsListRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler implementation

        async Task<OptionsListViewModel> IRequestHandler<CreateOptionsListCommand, OptionsListViewModel>.Handle(
            CreateOptionsListCommand request, CancellationToken cancellationToken)
        {
            var entry = _mapper.Map<OptionsList>(request);

            var optionList = await _repository.AddAsync(entry);
            return _mapper.Map<OptionsListViewModel>(optionList);
        }

        #endregion
    }
}