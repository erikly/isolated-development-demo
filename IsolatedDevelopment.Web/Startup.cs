using System;
using System.Collections.Generic;
using IsolatedDevelopment.Web.Dependencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IsolatedDevelopment.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Couldn't find a way to add middlewares to the beginning of the pipeline without this
        public static List<Type> FirstMiddlewares = new List<Type>();

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            // Add the integrated dependency
            services.AddTransient<IIntegratedDependency, IntegratedDependency>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            foreach (var middleware in FirstMiddlewares)
            {
                app.UseMiddleware(middleware);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}