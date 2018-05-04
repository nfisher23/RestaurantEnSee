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
using RestaurantEnSee.Areas.Admin.Models;
using RestaurantEnSee.Areas.Home.Middleware;
using RestaurantEnSee.Areas.Home.Models;
using RestaurantEnSee.Areas.Order.Models;

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
            services.AddTransient<IOrderCommunicationRepository, EFOrderCommunicationRepository>();
            services.AddScoped(provider => SessionShoppingCart.GetCart(provider));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();

            // in memory--means that multiple servers will not work for this implementation.
            // if you want to scale this to multiple servers, this will need to be changed.
            // see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.1&tabs=aspnetcore2x
            services.AddDistributedMemoryCache(); 
            services.AddSession();
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
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "",
                    template: "Order/{action=CartSummary}",
                    defaults: new { area = "Order", controller = "Order" });

                routes.MapRoute(name: "",
                    template: "RESAdmin/{action=Dashboard}",
                    defaults: new { area = "Admin", controller = "Admin" });

                routes.MapRoute(name: "",
                    template: "{action=Menu}",
                    defaults: new { area = "Home", controller = "Home" });
            });

        }
    }
}
