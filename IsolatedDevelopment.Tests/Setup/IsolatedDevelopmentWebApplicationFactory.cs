using IsolatedDevelopment.Tests.Middleware;
using IsolatedDevelopment.Tests.Stubs;
using IsolatedDevelopment.Web;
using IsolatedDevelopment.Web.Dependencies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IsolatedDevelopment.Tests.Setup
{
    public class IsolatedDevelopmentWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public IsolatedDevelopmentWebApplicationFactory()
        {
            Startup.FirstMiddlewares.Add(typeof(InjectCookieMiddleware));
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder) => ConfigureServices(builder);

        // Extracting a static method so that it may be used from the Isolated.Web assembly
        public static void ConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // The framework adds an antiforgery token. Ignore this in tests.
                services.AddMvc().AddRazorPagesOptions(options =>
                    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

                // replace integrated dependency with stub
                var descriptor = new ServiceDescriptor(typeof(IIntegratedDependency), typeof(IntegratedDependencyStub),
                    ServiceLifetime.Transient);
                services.Replace(descriptor);
            });
        }
    }
}
