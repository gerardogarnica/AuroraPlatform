using Aurora.Framework.Api;
using Aurora.Platform.Settings.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Settings.API.Controllers
{
    [ApiController]
    [Route("aurora/api/settings/options")]
    public class OptionsListController : AuroraControllerBase
    {
        #region Private members

        private readonly IOptionsListQueries _optionsListQueries;

        #endregion

        #region Constructors

        public OptionsListController(
            IOptionsListQueries optionsListQueries,
            ILogger<OptionsListController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
            _optionsListQueries = optionsListQueries ?? throw new ArgumentNullException(nameof(optionsListQueries));
        }

        #endregion

        #region Controller methods

        [HttpGet(Name = "GetByCode")]
        [ProducesResponseType(typeof(IReadOnlyList<OptionsListViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<OptionsListViewModel>>> GetByCode(string code, [FromQuery] bool onlyGetActiveItems)
        {
            var optionsList = await _optionsListQueries.GetByCodeAsync(code, onlyGetActiveItems);
            if (optionsList == null) return NoContent();

            return Ok(optionsList);
        }

        #endregion
    }
}