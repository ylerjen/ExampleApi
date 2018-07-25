using Microsoft.AspNetCore.Cors.Infrastructure;
using System;

namespace Example.Api.Middlewares
{
    /// <summary>
    /// This is a CorsMiddleware which provide some example CORS configuration
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.1#setting-up-cors"/>
    /// </summary>
    public class CorsMiddleware
    {
        private CorsOptions Options { get; }

        public CorsMiddleware(CorsOptions opts)
        {
            this.Options = opts;
        }

        /// <summary>
        /// Add the AllowSpecificOrigins policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddOriginPolicy()
        {
            this.Options.AddPolicy("AllowSpecificOrigins",
            builder =>
            {
                builder.WithOrigins("http://example.com", "http://www.contoso.com");
            });
        }

        /// <summary>
        /// Add the AllowAllOrigins policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddAllowAllOriginsPolicy()
        {

            this.Options.AddPolicy("AllowAllOrigins",
                 builder =>
                 {
                     builder.AllowAnyOrigin();
                 });
        }

        /// <summary>
        /// Add the AllowSpecificMethods policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddAllowSpecificMethodsPolicy()
        {

            this.Options.AddPolicy("AllowSpecificMethods",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .WithMethods("GET", "POST", "HEAD");
                    });
        }


        /// <summary>
        /// Add the AllowAllMethods policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddAllowAllMethodsPolicy()
        {
            this.Options.AddPolicy("AllowAllMethods",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .AllowAnyMethod();
                    });
        }


        /// <summary>
        /// Add the AllowHeaders policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddAllowHeadersPolicy()
        {
            this.Options.AddPolicy("AllowHeaders",
                builder =>
                {
                    builder.WithOrigins("http://example.com")
                           .WithHeaders("accept", "content-type", "origin", "x-custom-header");
                });
        }


        /// <summary>
        /// Add the AllowAllHeaders policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddAllowAllHeadersPolicy()
        {
            this.Options.AddPolicy("AllowAllHeaders",
            builder =>
            {
                builder.WithOrigins("http://example.com")
                       .AllowAnyHeader();
            });
        }


        /// <summary>
        /// Add the ExposeResponseHeaders policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddExposeResponseHeadersPolicy()
        {
            this.Options.AddPolicy("ExposeResponseHeaders",
            builder =>
            {
                builder.WithOrigins("http://example.com")
                       .WithExposedHeaders("x-custom-header");
            });
        }


        /// <summary>
        /// Add the AllowCredentials policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddAllowCredentialsPolicy()
        {
            this.Options.AddPolicy("AllowCredentials",
            builder =>
            {
                builder.WithOrigins("http://example.com")
                       .AllowCredentials();
            });
        }


        /// <summary>
        /// Add the SetPreflightExpiration policy to the <see cref="CorsOptions"/>
        /// </summary>
        public void AddSetPreflightExpirationPolicy()
        {
            this.Options.AddPolicy("SetPreflightExpiration",
            builder =>
            {
                builder.WithOrigins("http://example.com")
                       .SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
            });
        }

    }
}
