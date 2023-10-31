using Aurora.Framework.Api;
using Aurora.Platform.Security.Application.Applications.Queries.GetApplications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ApplicationEntity = Aurora.Platform.Security.Domain.Entities.Application;

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
        [ProducesResponseType(typeof(IReadOnlyList<ApplicationEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ApplicationEntity>>> GetList()
        {
            var response = await _mediator.Send(new GetApplicationsQuery());
            return Ok(response);
        }
    }
}