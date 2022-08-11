using Aurora.Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Aurora.Framework.Api
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException e)
            {
                var response = new ErrorDetailResponse(
                    StatusCodes.Status400BadRequest,
                    ErrorDetailCategory.BusinessValidation);

                response.AddErrorMessage(e.ErrorType, e.Message);

                await HandleExceptionAsync(context, response);
            }
            catch (ValidationException e)
            {
                var response = new ErrorDetailResponse(
                    StatusCodes.Status400BadRequest,
                    ErrorDetailCategory.ModelValidation);

                foreach (var error in e.Errors)
                {
                    response.AddErrorMessage(e.GetType().Name, error);
                }

                await HandleExceptionAsync(context, response);
            }
            catch (Exception e)
            {
                var response = new ErrorDetailResponse(
                    StatusCodes.Status500InternalServerError,
                    ErrorDetailCategory.Error);

                response.AddErrorMessage(e.GetType().Name, e.Message);

                if (e.InnerException != null)
                {
                    response.AddErrorMessage(
                        e.InnerException.GetType().Name,
                        e.InnerException.Message);
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