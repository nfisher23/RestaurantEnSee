using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantEnSee.Areas.Home.Models;

namespace RestaurantEnSee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            IHostingEnvironment env = (IHostingEnvironment)host.Services.GetService(typeof(IHostingEnvironment));
            SeedData.EnsureDbPopulated(host.Services, env);

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(opts => opts.ValidateScopes = false)
                .Build();
    }
}
