using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DFF.Freedom.Web.Host.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel() //使用KestrelServer的服务器。
                .UseContentRoot(Directory.GetCurrentDirectory()) //使用
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
