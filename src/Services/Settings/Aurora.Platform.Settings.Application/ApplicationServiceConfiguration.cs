using Aurora.Framework.Validations;
using Aurora.Platform.Settings.Application.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aurora.Platform.Settings.Application
{
    public static class ApplicationServiceConfiguration
    {
        public static IServiceCollection AddSettingsApplicationServices(this IServiceCollection services)
        {
            // Queries interfaces
            services.AddScoped<IOptionsListQueries, OptionsListQueries>();

            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Validations
            services.AddValidationServices();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}