using System;
using Example.Domain.Entities;
using Example.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Example.Repository.Repositories;
using Example.Api.Commands;
using Example.Api.Models;
using Example.Helpers;
using Example.Api.Middlewares;

using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;

namespace Example.Api
{
    public class Startup
    {
        private static readonly Version ApiVersion = new Version(1, 0, 0);

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opts =>
            {
                var corsMw = new CorsMiddleware(opts);
                corsMw.AddAllowAllOriginsPolicy();
            });
            services.AddMvc(
                setupAction =>
                    {
                        setupAction.ReturnHttpNotAcceptable = true;
                        setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                        setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
                    });
            services.AddScoped<IUsersService>(provider => new UsersService(new DateTimeProvider(), new UsersRepository()));
            services.AddScoped<IEventsService>(provider => new EventsService(new EventsRepository()));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);

            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Example API", Version = "v1" });
                c.OperationFilter<ExamplesOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseResponseBodyFormatterMiddleWare();
            }
            else
            {
                app.UseResponseBodyFormatterMiddleWare();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>()
                    .ForMember(destUser => destUser.Gender, opt => opt.MapFrom(src => src.Gender));
                cfg.CreateMap<UserForCreationDto, User>()
                    .ForMember(destUser => destUser.Gender, opt => opt.MapFrom(src => src.Gender));
            });
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API V1");
               // c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
