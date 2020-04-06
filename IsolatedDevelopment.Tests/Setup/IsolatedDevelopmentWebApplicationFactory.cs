using IsolatedDevelopment.Dependencies;
using IsolatedDevelopment.Tests.Stubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IsolatedDevelopment.Tests.Setup
{
    public class IsolatedDevelopmentWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) => ConfigureServices(builder);

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
