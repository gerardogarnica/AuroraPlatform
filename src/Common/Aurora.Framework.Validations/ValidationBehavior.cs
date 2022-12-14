using FluentValidation;
using MediatR;

namespace Aurora.Framework.Validations
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Private members

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        #endregion

        #region Constructors

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        #endregion

        #region IPipelineBehavior interface implementation

        async Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(
            TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var failures = _validators
                    .Select(v => v.Validate(request))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .Select(f => f.ErrorMessage)
                    .ToList();

                if (failures.Any())
                {
                    throw new Exceptions.ValidationException(failures);
                }
            }

            return await next();
        }

        #endregion
    }
}