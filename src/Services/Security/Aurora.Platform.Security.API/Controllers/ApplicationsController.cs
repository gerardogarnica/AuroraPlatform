using Aurora.Framework.Api;
using Aurora.Framework.Identity;
using Aurora.Platform.Security.Application.Applications.Queries.GetApplications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Security.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("aurora/api/security/v1/applications")]
    public class ApplicationsController : AuroraControllerBase
    {
        #region Constructors

        public ApplicationsController(
            ILogger<ApplicationsController> logger,
            IMediator mediator)
            : base(logger, mediator) { }

        #endregion

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ApplicationInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ApplicationInfo>>> GetList()
        {
            var response = await _mediator.Send(new GetApplicationsQuery());
            return Ok(response);
        }
    }
}