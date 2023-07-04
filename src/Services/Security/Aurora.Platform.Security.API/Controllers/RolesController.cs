using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Application.Roles.Commands.CreateRole;
using Aurora.Platform.Security.Application.Roles.Commands.UpdateRole;
using Aurora.Platform.Security.Application.Roles.Queries.GetRoleById;
using Aurora.Platform.Security.Application.Roles.Queries.GetRoles;
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

        [HttpGet]
        [ProducesResponseType(typeof(RoleInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<RoleInfo>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] string application, [FromQuery] bool onlyActives)
        {
            var response = await _mediator.Send(
                new GetRolesQuery { PagedViewRequest = viewRequest, Application = application, OnlyActives = onlyActives });

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Create([FromBody] CreateRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Update([FromBody] UpdateRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(response);
        }
    }
}