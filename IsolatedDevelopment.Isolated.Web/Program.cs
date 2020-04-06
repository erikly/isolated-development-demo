using System.IO;
using IsolatedDevelopment.Tests.Middleware;
using Microsoft.AspNetCore.Hosting;
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
                    Startup.FirstMiddlewares.Add(typeof(InjectCookieMiddleware));
                    webBuilder.UseStartup<Startup>();
                    var webProjectName = typeof(Startup).Assembly.GetName().Name;
                    var webProjectPath = $"{Directory.GetCurrentDirectory()}/../{webProjectName}";
                    webBuilder.UseContentRoot(webProjectPath);
                });
        }
    }
}
