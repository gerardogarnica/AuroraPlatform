using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Framework.Platform.Options;
using Aurora.Platform.Settings.Application.Options.Commands.CreateOption;
using Aurora.Platform.Settings.Application.Options.Commands.SaveItem;
using Aurora.Platform.Settings.Application.Options.Commands.UpdateOption;
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
        [ProducesResponseType(typeof(OptionsCatalog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OptionsCatalog>> GetByCode(string code, [FromQuery] bool onlyGetActiveItems)
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
        [ProducesResponseType(typeof(OptionsCatalog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<OptionsCatalog>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] string search, [FromQuery] bool onlyVisibles)
        {
            var response = await _mediator.Send(
                new GetOptionsQuery
                {
                    PagedViewRequest = viewRequest,
                    Search = search,
                    OnlyVisibles = onlyVisibles
                });

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OptionsCatalog), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OptionsCatalog>> Create([FromBody] CreateOptionCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OptionsCatalog), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OptionsCatalog>> Update([FromBody] UpdateOptionCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(string.Empty, response);
        }

        [HttpPost("item")]
        [ProducesResponseType(typeof(OptionsCatalog), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OptionsCatalog>> SaveItem([FromBody] SaveItemCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }
    }
}