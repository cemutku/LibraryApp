using AutoMapper;
using LibraryApp.API.Mapper;
using LibraryApp.Data;
using LibraryApp.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace LibraryApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<LibraryAppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("LibraryContext")));

            services.AddScoped<ILibraryAppDataRepository, LibraryAppDataRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("LibraryAppOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Library API",
                        Version = "1",
                        Description = "Using this sample API you can access articles and books.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Name = "Cem Utku",
                            Email = "",
                            Url = new Uri("https://github.com/cemutku")
                        }
                    });

                // give the name of xml comment file
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                // full path
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/LibraryAppOpenAPISpecification/swagger.json", 
                    "Library API");

                setupAction.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
