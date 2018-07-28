using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Example.Domain.Exceptions;
using Example.Domain.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Example.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResponseBodyFormatterMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseBodyFormatterMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await this._next(httpContext);
            }
            catch (ValidationException validationException)
            {
                httpContext.Response.Clear();

                httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                httpContext.Response.ContentType = httpContext.Request.ContentType;

                var body = JsonConvert.SerializeObject(validationException.BusinessValidationErrors);
                await httpContext.Response.WriteAsync(body);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ResponseBodyFormatterMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseBodyFormatterMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseBodyFormatterMiddleware>();
        }
    }
}
