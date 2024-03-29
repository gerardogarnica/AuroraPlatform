﻿using Aurora.Framework.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Aurora.Framework.Api
{
    public class ApiHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IIdentityHandler identityHandler)
        {
            try
            {
                var requestHeader = context.Request.Headers["Authorization"];

                if (requestHeader.Any())
                {
                    var token = requestHeader.FirstOrDefault().GetLastSplit(" ");

                    identityHandler.ValidateToken(token);
                    context.Items["UserInfo"] = identityHandler.UserInfo;
                }

                await _next(context);
            }
            catch (Exception e)
            {
                var response = new ErrorDetailResponse();

                switch (e)
                {
                    case ApiAuthorizationException:
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        response.ErrorCategory = ErrorDetailCategory.Authorization;
                        response.AddErrorMessage(e.GetType().Name, e.Message);
                        break;

                    case BusinessException:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.ErrorCategory = ErrorDetailCategory.BusinessValidation;
                        response.AddErrorMessage(((BusinessException)e).ErrorType, e.Message);

                        break;

                    case ValidationException:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.ErrorCategory = ErrorDetailCategory.ModelValidation;
                        foreach (var error in ((ValidationException)e).Errors)
                        {
                            response.AddErrorMessage(e.GetType().Name, error);
                        }

                        break;

                    default:
                        response.AddErrorMessage(e.GetType().Name, e.Message);
                        if (e.InnerException != null)
                        {
                            response.AddErrorMessage(
                                e.InnerException.GetType().Name,
                                e.InnerException.Message);
                        }

                        break;
                }

                await HandleExceptionAsync(context, response);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ErrorDetailResponse response)
        {
            var jsonResponse = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}