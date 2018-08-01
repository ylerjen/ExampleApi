using System.Net;
using System.Threading.Tasks;
using Example.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Example.Api.Middlewares
{
    /// <summary>
    /// This middleware is used to format the error thrown by a business validation to a specific response json
    /// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    /// </summary>
    public class ResponseBodyFormatterMiddleware
    {
        private readonly RequestDelegate next;

        public ResponseBodyFormatterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
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

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class ResponseBodyFormatterMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseBodyFormatterMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseBodyFormatterMiddleware>();
        }
    }
}
