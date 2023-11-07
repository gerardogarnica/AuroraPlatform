using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Platform.Settings.Application.Options;
using Aurora.Platform.Settings.Application.Options.Commands.CreateOption;
using Aurora.Platform.Settings.Application.Options.Queries.GetOptionByCode;
using Aurora.Platform.Settings.Application.Options.Queries.GetOptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Settings.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("aurora/api/settings/v1/options")]
    public class OptionsController : AuroraControllerBase
    {
        #region Constructors

        public OptionsController(
            ILogger<OptionsController> logger,
            IMediator mediator)
            : base(logger, mediator) { }

        #endregion

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(OptionsCatalogModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OptionsCatalogModel>> GetByCode(string code, [FromQuery] bool onlyGetActiveItems)
        {
            var response = await _mediator.Send(
                new GetOptionByCodeQuery
                {
                    Code = code,
                    OnlyActiveItems = onlyGetActiveItems
                });
            if (response == null) return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OptionsCatalogModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<OptionsCatalogModel>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] string application,
            [FromQuery] string search, [FromQuery] bool excludeGlobals, [FromQuery] bool onlyVisibles)
        {
            var response = await _mediator.Send(
                new GetOptionsQuery
                {
                    PagedViewRequest = viewRequest,
                    Application = application,
                    Search = search,
                    ExcludeGlobals = excludeGlobals,
                    OnlyVisibles = onlyVisibles
                });

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OptionsCatalogModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OptionsCatalogModel>> Create([FromBody] CreateOptionCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }
    }
}