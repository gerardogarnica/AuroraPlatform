using Aurora.Framework.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aurora.Framework.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authorization for allow anonymous objects
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any()) return;

            // Thrown an error if there not a registered user
            if ((UserInfo)context.HttpContext.Items["UserInfo"] == null)
                throw new ApiAuthorizationException();
        }
    }
}