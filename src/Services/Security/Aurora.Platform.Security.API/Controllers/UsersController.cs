using Aurora.Framework.Api;
using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Application.Users.Queries.GetUserByLogin;
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

        [HttpGet("{loginName}")]
        [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserInfo>> GetByLoginName(string loginName)
        {
            var response = await _mediator.Send(new GetUserByLoginQuery { LoginName = loginName });
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
    }
}