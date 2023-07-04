using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Application.Users.Commands.CreateUser;
using Aurora.Platform.Security.Application.Users.Commands.UpdateUser;
using Aurora.Platform.Security.Application.Users.Commands.UpdateUserRole;
using Aurora.Platform.Security.Application.Users.Commands.UpdateUserStatus;
using Aurora.Platform.Security.Application.Users.Queries.GetUserByEmail;
using Aurora.Platform.Security.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Security.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("aurora/api/security/v1/users")]
    public class UsersController : AuroraControllerBase
    {
        #region Constructors

        public UsersController(
            ILogger<UsersController> logger,
            IMediator mediator)
            : base(logger, mediator) { }

        #endregion

        [HttpGet("{email}")]
        [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserInfo>> GetByEmail(string email)
        {
            var response = await _mediator.Send(new GetUserByEmailQuery { Email = email });
            if (response == null) return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<UserInfo>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] int roleId, [FromQuery] bool onlyActives)
        {
            var response = await _mediator.Send(
                new GetUsersQuery { PagedViewRequest = viewRequest, RoleId = roleId, OnlyActives = onlyActives });

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Update([FromBody] UpdateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        [HttpPut("{email}/activate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Activate(string email)
        {
            var command = new UpdateUserStatusCommand()
            {
                Email = email,
                IsActive = true
            };

            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        [HttpPut("{email}/deactivate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Deactivate(string email)
        {
            var command = new UpdateUserStatusCommand()
            {
                Email = email,
                IsActive = false
            };

            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        [HttpPut("{email}/roles/add")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddRole(string email, [FromBody] int roleId)
        {
            var command = new UpdateUserRoleCommand()
            {
                Email = email,
                RoleId = roleId,
                IsAddAction = true
            };

            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        [HttpPut("{email}/roles/remove")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> RemoveRole(string email, [FromBody] int roleId)
        {
            var command = new UpdateUserRoleCommand()
            {
                Email = email,
                RoleId = roleId,
                IsAddAction = false
            };

            var response = await _mediator.Send(command);
            return Accepted(response);
        }
    }
}