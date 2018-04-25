using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantEnSee.Areas.Home.Middleware;
using RestaurantEnSee.Areas.Home.Models;

namespace RestaurantEnSee
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opts => opts.UseInMemoryDatabase("Data Source=developmentDatabase.sqlite"));

            services.AddTransient<IMenuRepository, EFMenuRepository>();

            services.AddMvc();
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseStatusCodePages();
            }

            app.UseMiddleware<ResolveImageMiddleware>();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {

                routes.MapRoute(name: "",
                    template: "Order/{action=CartSummary}",
                    defaults: new { area = "Order", controller = "Order" });

                routes.MapRoute(name: "",
                    template: "{area=Home}/{action=Menu}",
                    defaults: new { controller = "Home" });
            });

        }
    }
}
