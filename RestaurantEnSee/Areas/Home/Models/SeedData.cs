using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantEnSee.Areas.Home.Models;
using System.IO;

namespace RestaurantEnSee.Areas.Home.Models
{
    public static class SeedData
    {
        public static void EnsureDevelopmentDbPopulated(IServiceProvider provider,
            IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                using (AppDbContext context = provider.GetRequiredService<AppDbContext>())
                {
                    context.Database.EnsureCreated();

                    context.Menus.Add(CreateDevelopmentMenu());
                    context.Menus.Add(CreateDevelopmentMenu(2));
                    context.AdminEmails.Add(new Admin.Models.Email.EmailConfiguration
                    {
                        SmtpPassword = "",
                        SmtpServer = "",
                        SmtpUsername = ""
                    });

                    context.SaveChanges();
                }
            }
        }


        public static Menu CreateDevelopmentMenu(int num = 1)
        {
            Menu m = new Menu
            {
                Categories = GenerateDevelopmentCategories(),
                MenuName = $"Default Menu {num}",
                IsActiveMenu = num == 1 ? true : false
            };

            return m;
        }

        private static List<FoodCategory> GenerateDevelopmentCategories()
        {
            List<FoodCategory> cats = new List<FoodCategory>();

            cats.Add(new FoodCategory
            {
                Title = "Sand-witches",
                FoodItems = new List<MenuItem>()
            });
            cats.Add(new FoodCategory
            {
                Title = "Soups",
                FoodItems = new List<MenuItem>()
            });
            cats.Add(new FoodCategory
            {
                Title = "Bowls",
                FoodItems = new List<MenuItem>()
            });
            cats.Add(new FoodCategory
            {
                Title = "Salads",
                FoodItems = new List<MenuItem>()
            });


            for (int i = 0; i < cats.Count; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    cats[i].FoodItems.Add(
                        new MenuItem
                        {
                            Title = $"{cats[i].Title.Substring(0, cats[i].Title.Length - 1)} #{j}",
                            Description = $"The best {cats[i].Title} no {j} around",
                            Picture = GetDevelopmentPhoto(),
                            PriceBeforeTax = (decimal)(10.95 + i)
                        });
                }
            }

            return cats;
        }


        private static Photo GetDevelopmentPhoto()
        {
            string name = "food-outline";
            string ext = ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "seed", name + ext);
            Photo p = new Photo
            {
                FullTitle = name + ext,
                Content = GetDevelopmentPhotoContent(path)
            };

            return p;
        }

        private static byte[] GetDevelopmentPhotoContent(string path)
        {
            var bytes = System.IO.File.ReadAllBytes(path);
            return bytes; // stub
        }
    }
}
