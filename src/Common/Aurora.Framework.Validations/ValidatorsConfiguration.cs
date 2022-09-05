using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Validations
{
    public static class ValidatorsConfiguration
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}