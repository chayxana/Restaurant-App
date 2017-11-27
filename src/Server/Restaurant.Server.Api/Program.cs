using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Restaurant.Server.Api
{
	[ExcludeFromCodeCoverage]
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args)
		{
            return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 6000);
                    options.Listen(IPAddress.Loopback, 6200, listenOptions =>
                    {
                        listenOptions.UseHttps("restaurantcert.pfx", "Test123");
                        listenOptions.UseConnectionLogging();
                    });
                }).Build();
		}
	}
}