using Aurora.Framework.Api;
using Aurora.Framework.Identity;
using Aurora.Platform.Security.Application.Identity.Commands.ChangePassword;
using Aurora.Platform.Security.Application.Identity.Commands.RefreshToken;
using Aurora.Platform.Security.Application.Identity.Commands.UserLogin;
using Aurora.Platform.Security.Application.Identity.Commands.UserLogout;
using Aurora.Platform.Security.Application.Identity.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Security.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("aurora/api/security/v1/auth")]
    public class AuthController : AuroraControllerBase
    {
        #region Constructors

        public AuthController(
            ILogger<AuthController> logger,
            IMediator mediator)
            : base(logger, mediator) { }

        #endregion

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentityToken>> Login(
            [FromHeader] string application, [FromBody] UserCredentials credentials)
        {
            var command = new UserLoginCommand
            {
                Application = application,
                Email = credentials.Email,
                Password = credentials.Password
            };

            var identityAccess = await _mediator.Send(command);
            return Created(string.Empty, identityAccess);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Logout()
        {
            var command = new UserLogoutCommand();
            var response = await _mediator.Send(command);

            return Created(string.Empty, response);
        }

        [AllowAnonymous]
        [HttpPost("recovery")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> RecoveryPassword([FromBody] string credentials)
        {
            return Created(string.Empty, null);
        }

        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(string.Empty, response);
        }

        [HttpPut("refresh-token")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(string.Empty, response);
        }

        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserInfo>> GetProfile()
        {
            var command = new GetProfileQuery();
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}