﻿using Microsoft.EntityFrameworkCore;
using RestaurantEnSee.Areas.Admin.Models.Email;
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
        public DbSet<Photo> Photos { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<EmailConfiguration> AdminEmails { get; set; }
    }
}
