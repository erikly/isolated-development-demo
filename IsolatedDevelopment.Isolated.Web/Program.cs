using System.IO;
using IsolatedDevelopment.Tests.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace IsolatedDevelopment.Isolated.Web
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    IsolatedDevelopmentWebApplicationFactory.ConfigureServices(webBuilder);
                    var webProjectName = typeof(Startup).Assembly.GetName().Name;
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                        config.SetFileProvider(
                            new PhysicalFileProvider($"{Directory.GetCurrentDirectory()}/../{webProjectName}")));
                    webBuilder.UseWebRoot($"../{webProjectName}/wwwroot");
                });
        }
    }
}
