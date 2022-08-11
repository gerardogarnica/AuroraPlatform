using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aurora.Framework.Api
{
    [ProducesErrorResponseType(typeof(ErrorDetailResponse))]
    public abstract class AuroraControllerBase : ControllerBase
    {
        #region Private members

        protected readonly ILogger<AuroraControllerBase> _logger;

        #endregion

        #region Constructors

        public AuroraControllerBase(ILogger<AuroraControllerBase> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion
    }
}