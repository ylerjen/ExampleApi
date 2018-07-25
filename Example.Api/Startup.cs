using Example.Api.Models;
using Example.Domain.Entities;
using Example.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Example.Repository.Repositories;
using Example.Api.Commands;
using Example.Helpers;
using Example.Api.Middlewares;

namespace Example.Api
{
    public class Startup
    {
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
            services.AddMvc();
            services.AddScoped<IAuthorsService>(provider => new AuthorsService(new DateTimeProvider(), new AuthorsRepository()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
                cfg.CreateMap<AuthorForCreationDto, Author>();
            });

            app.UseMvc();
        }
    }
}
