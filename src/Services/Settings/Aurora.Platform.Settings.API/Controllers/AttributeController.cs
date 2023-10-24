using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Platform.Settings.Application.Attributes;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetSettingByCode;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetSettings;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetValueByCode;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetValues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Settings.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("aurora/api/settings/v1/attributes")]
    public class AttributeController : AuroraControllerBase
    {
        #region Constructors

        public AttributeController(
            ILogger<AttributeController> logger,
            IMediator mediator)
            : base(logger, mediator) { }

        #endregion

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(AttributeSettingModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttributeSettingModel>> GetSettingByCode(string code)
        {
            var response = await _mediator.Send(new GetSettingByCodeQuery { Code = code });
            if (response == null) return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AttributeSettingModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<AttributeSettingModel>>> GetSettingsList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] string scope)
        {
            var response = await _mediator.Send(
                new GetSettingsQuery
                {
                    PagedViewRequest = viewRequest,
                    Scope = scope
                });

            return Ok(response);
        }

        [HttpGet("values/{code}")]
        [ProducesResponseType(typeof(AttributeValueModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttributeValueModel>> GetValueByCode(string code, [FromQuery] int relationshipId)
        {
            var response = await _mediator.Send(
                new GetValueByCodeQuery
                {
                    Code = code,
                    RelationshipId = relationshipId
                });
            if (response == null) return NoContent();

            return Ok(response);
        }

        [HttpGet("values/{scopeType}/{relationshipId}")]
        [ProducesResponseType(typeof(AttributeSettingModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<AttributeValueModel>>> GetValues(string scopeType, int relationshipId)
        {
            var response = await _mediator.Send(
                new GetValuesQuery
                {
                    ScopeType = scopeType,
                    RelationshipId = relationshipId
                });

            return Ok(response);
        }
    }
}