using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Application.Roles.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Security.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("aurora/api/security/v1/roles")]
    public class RolesController : AuroraControllerBase
    {
        #region Constructors

        public RolesController(
            ILogger<RolesController> logger,
            IMediator mediator)
            : base(logger, mediator) { }

        #endregion

        [HttpGet("{roleId}")]
        [ProducesResponseType(typeof(RoleInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleInfo>> GetById(int roleId)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery { RoleId = roleId });
            if (response == null) return NoContent();

            return Ok(response);
        }

    }
}