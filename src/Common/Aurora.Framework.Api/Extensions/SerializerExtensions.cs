using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Aurora.Framework.Api
{
    public static class SerializerExtensions
    {
        public static IServiceCollection AddStringEnumConverter(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(a =>
            {
                a.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return services;
        }
    }
}