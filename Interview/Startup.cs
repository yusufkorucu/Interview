using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interview.Business.Abstract;
using Interview.Business.Concrete;
using Interview.DAL.Abstract;
using Interview.DAL.Concrete.EntityFramework;
using Interview.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Interview
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
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddScoped<IMailer, MailerManager>();
            services.AddSwaggerGen(s => {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Web API",
                    Version = "v1"
                });

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

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
