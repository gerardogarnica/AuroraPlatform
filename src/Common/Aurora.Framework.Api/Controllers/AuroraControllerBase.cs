using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aurora.Framework.Api
{
    [ProducesErrorResponseType(typeof(ErrorDetailResponse))]
    public abstract class AuroraControllerBase : ControllerBase
    {
        #region Private members

        protected readonly ILogger<AuroraControllerBase> _logger;
        protected readonly IMediator? _mediator;

        #endregion

        #region Constructors

        public AuroraControllerBase(ILogger<AuroraControllerBase> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public AuroraControllerBase(
            ILogger<AuroraControllerBase> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion
    }
}