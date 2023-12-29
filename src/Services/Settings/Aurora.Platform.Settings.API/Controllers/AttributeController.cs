using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Platform.Settings.Application.Attributes.Commands.CreateSetting;
using Aurora.Platform.Settings.Application.Attributes.Commands.SaveValue;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetSettingByCode;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetSettings;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetValueByCode;
using Aurora.Platform.Settings.Application.Attributes.Queries.GetValues;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AttributeSettingModel = Aurora.Framework.Platform.Attributes.AttributeSetting;
using AttributeValueModel = Aurora.Framework.Platform.Attributes.AttributeValue;

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

        [HttpPost]
        [ProducesResponseType(typeof(AttributeSettingModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttributeSettingModel>> CreateSetting([FromBody] CreateSettingCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
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
        [ProducesResponseType(typeof(AttributeValueModel), StatusCodes.Status200OK)]
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

        [HttpPost("values")]
        [ProducesResponseType(typeof(AttributeValueModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttributeValueModel>> SaveValue([FromBody] SaveValueCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }
    }
}