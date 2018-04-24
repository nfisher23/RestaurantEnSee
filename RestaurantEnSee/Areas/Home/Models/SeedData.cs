using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantEnSee.Areas.Home.Models;

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
                    context.Categories.AddRange(GenerateDevelopmentCategories());

                    context.SaveChanges();
                }
            }
        }


        public static Menu CreateDevelopmentMenu()
        {
            Menu m = new Menu
            {
                Categories = GenerateDevelopmentCategories()
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
                            Title = $"{cats[i].Title.Substring(0, cats[i].Title.Length - 2)} #{j}",
                            Description = $"The best {cats[i].Title} no {j} around",
                            Picture = GetDevelopmentPhoto()
                        });
                }
            }

            return cats;
        }


        private static Photo GetDevelopmentPhoto()
        {
            Photo p = new Photo
            {
                Title = "Base Photo",
                Extension = ".jpg",
                Content = GetDevelopmentPhotoContent()
            };

            return p;
        }

        private static byte[] GetDevelopmentPhotoContent()
        {
            return new byte[1]; // stub
        }
    }
}
