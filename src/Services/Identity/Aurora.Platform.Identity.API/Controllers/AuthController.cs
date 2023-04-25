using Aurora.Framework.Api;
using Aurora.Platform.Security.Application.UserLogin;
using Aurora.Platform.Security.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Identity.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/v1/auth")]
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
        public async Task<ActionResult<IdentityAccess>> Login([FromBody] UserLoginCommand command)
        {
            var identityAccess = await _mediator.Send(command);
            return Created(string.Empty, identityAccess);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Logout()
        {
            return Created(string.Empty, null);
        }

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
        public async Task<ActionResult<bool>> ChangePassword([FromBody] string credentials)
        {
            return Accepted(string.Empty, null);
        }

        [HttpPut("refresh-token")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> RefreshToken([FromBody] string credentials)
        {
            return Accepted(string.Empty, null);
        }

        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetProfile()
        {
            return Ok(string.Empty);
        }

    }
}
