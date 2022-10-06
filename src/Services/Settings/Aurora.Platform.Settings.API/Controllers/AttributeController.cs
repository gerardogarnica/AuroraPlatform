using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Platform.Settings.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Settings.API.Controllers
{
    [ApiController]
    [Route("aurora/api/settings/attributes")]
    public class AttributeController : AuroraControllerBase
    {
        #region Private members

        private readonly IAttributeQueries _attributeQueries;

        #endregion

        #region Constructors

        public AttributeController(
            IAttributeQueries attributeQueries,
            ILogger<AttributeController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
            _attributeQueries = attributeQueries ?? throw new ArgumentNullException(nameof(attributeQueries));
        }

        #endregion

        #region Controller methods

        [HttpGet("{code}", Name = "GetSettingByCode")]
        [ProducesResponseType(typeof(AttributeSettingModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttributeSettingModel>> GetSettingByCode(string code)
        {
            var attributeSetting = await _attributeQueries.GetSettingAsync(code);
            if (attributeSetting == null) return NoContent();

            return Ok(attributeSetting);
        }

        [HttpGet(Name = "GetSettingsList")]
        [ProducesResponseType(typeof(PagedCollection<AttributeSettingModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<AttributeSettingModel>>> GetSettingsList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] string scopeType)
        {
            var attributeSettings = await _attributeQueries.GetSettingListAsync(viewRequest, scopeType);
            return Ok(attributeSettings);
        }

        #endregion
    }
}