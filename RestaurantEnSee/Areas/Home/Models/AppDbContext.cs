using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base (opts)
        { }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<FoodCategory> Categories { get; set; }
    }
}
