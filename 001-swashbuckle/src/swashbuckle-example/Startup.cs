using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace swashbuckle_example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // create JSON file "database" if it doesn't exist
            if(!System.IO.File.Exists("jsonFile.json"))
                System.IO.File.WriteAllText("jsonFile.json","[]");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            /*
            // tag::swaggerConfigureServices[]
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Sports API", Version = "v1"});
            });
            // end::swaggerConfigureServices[]
            */

            /*
            // tag::swaggerConfigureServicesInfo[]
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Sports API",
                    Version = "v1",
                    Description = "An API to list and add sports teams",
                    TermsOfService = "This is just an example, not for production!",
                    Contact = new Contact
                    {
                        Name = "Matthew Groves",
                        Url = "https://crosscuttingconcerns.com"
                    },
                    License = new License
                    {
                        Name = "Apache 2.0",
                        Url = "http://www.apache.org/licenses/LICENSE-2.0.html"
                    }
                });
            });
            // end::swaggerConfigureServicesInfo[]
            */

            // tag::swaggerConfigureServicesWithXMLComments[]
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Sports API", Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "swashbuckle-example.xml");
                c.IncludeXmlComments(filePath);
            });
            // end::swaggerConfigureServicesWithXMLComments[]
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // tag::swaggerConfigure[]
            app.UseSwagger();
            // end::swaggerConfigure[]

            // tag::swaggerUIConfigure[]
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sports API v1");
            });
            // end::swaggerUIConfigure[]
        }
    }
}
