using Aurora.Framework.Api;
using Aurora.Platform.Applications.API.Models;
using Aurora.Platform.Applications.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Platform.Applications.API.Controllers
{
    [ApiController]
    [Route("aurora/api/applications")]
    public class ApplicationsController : AuroraControllerBase
    {
        private readonly IApplicationRepository _repository;

        public ApplicationsController(
            IApplicationRepository repository,
            ILogger<ApplicationsController> logger)
            : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet(Name = "GetApplications")]
        [ProducesResponseType(typeof(IReadOnlyList<Application>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<Application>>> GetAll()
        {
            var applications = await _repository.GetListAsync();
            return Ok(applications);
        }

        [HttpGet("{code}", Name = "GetApplication")]
        [ProducesResponseType(typeof(Application), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Application>> Get(string code)
        {
            var application = await _repository.GetByCodeAsync(code);
            if (application == null) return NoContent();

            return Ok(application);
        }
    }
}