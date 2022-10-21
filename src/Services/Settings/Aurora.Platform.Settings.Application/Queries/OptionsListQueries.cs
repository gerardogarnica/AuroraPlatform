using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;

namespace Aurora.Platform.Settings.Application.Queries
{
    public interface IOptionsListQueries
    {
        Task<OptionsListViewModel> GetByCodeAsync(string code, bool onlyActiveItems);
    }

    public class OptionsListQueries : IOptionsListQueries
    {
        #region Private members

        private readonly IOptionsListRepository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public OptionsListQueries(IOptionsListRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IOptionsListQueries implementation

        async Task<OptionsListViewModel> IOptionsListQueries.GetByCodeAsync(string code, bool onlyActiveItems)
        {
            var optionList = await _repository.GetByCodeAsync(code);
            if (optionList == null) return null;

            if (onlyActiveItems)
            {
                optionList.Items.ToList().RemoveAll(x => x.IsActive == false);
            }

            return _mapper.Map<OptionsListViewModel>(optionList);
        }

        #endregion
    }
}