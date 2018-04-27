using Microsoft.AspNetCore.Http;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Middleware
{
    public class ResolveImageMiddleware
    {
        private readonly RequestDelegate _next;

        public ResolveImageMiddleware(RequestDelegate deg)
        {
            _next = deg;
        }

        public async Task InvokeAsync(HttpContext httpContext, IMenuRepository menuRepo)
        {
            var path = httpContext.Request.Path;
            if (path.HasValue && path.Value.StartsWith("/dbimgs/"))
            {
                httpContext.Request.Path = "/GetImageFromDatabase";
                httpContext.Request.QueryString =
                    new QueryString("?fullImgTitle=" + path.Value.Replace("/dbimgs/", ""));
            }

            await _next(httpContext);
        }
    }
}
