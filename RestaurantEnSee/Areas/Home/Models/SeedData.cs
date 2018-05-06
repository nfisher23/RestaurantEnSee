using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantEnSee.Areas.Home.Models;
using System.IO;
using RestaurantEnSee.Areas.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace RestaurantEnSee.Areas.Home.Models
{
    public static class SeedData
    {
        public static void EnsureDbPopulated(IServiceProvider provider, 
            IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                EnsureDevelopmentDbPopulated(provider, environment);
            }
            else if (environment.IsProduction())
            {
                EnsureProductionDbPopulated(provider, environment);
            }
            EnsureIdentityPopulated(provider, environment).Wait();
        }

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

        public static void EnsureProductionDbPopulated(IServiceProvider provider,
            IHostingEnvironment environment)
        {
            if (environment.IsProduction())
            {
                using (AppDbContext context = provider.GetRequiredService<AppDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (context.Menus.Count() == 0)
                    {
                        context.Menus.Add(CreateProductionSeedMenu());
                    }
                    if (context.AdminEmails.Count() == 0)
                    {
                        context.AdminEmails.Add(new Admin.Models.Email.EmailConfiguration
                        {
                            SmtpPassword = "",
                            SmtpServer = "",
                            SmtpUsername = ""
                        });
                    }

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

        public static Menu CreateProductionSeedMenu()
        {
            Menu m = new Menu
            {
                Categories = new List<FoodCategory>
                {
                    new FoodCategory
                    {
                        FoodItems = new List<MenuItem>
                        {
                            new MenuItem
                            {
                                Description = "Your first Menu Item",
                                Picture = GetDevelopmentPhoto(),
                                PriceBeforeTax = 9.99M,
                                Title = "Menu Item"
                            }
                        },
                        Title = "Your First Category"
                    }
                },
                MenuName = "Default Menu 1",
                IsActiveMenu = true
            };
            return m;
        }



        public static string DefaultUsername = "RESEnSeeUser";
        public static string DefaultPassword = "RESDefaultPassword1!";

        public static async Task EnsureIdentityPopulated(IServiceProvider provider,
            IHostingEnvironment environment)
        {
            using (var context = provider.GetRequiredService<AppIdentityDbContext>())
            {
                context.Database.EnsureCreated();
                var _userManager = provider.GetRequiredService<UserManager<AppUser>>();

                if (!_userManager.Users.Any())
                {
                    AppUser user = new AppUser
                    {
                        UserName = DefaultUsername
                    };
                    await _userManager.CreateAsync(user, DefaultPassword);
                }
            }
        }

        public static bool IsFirstSignIn(string username, string password)
        {
            return (username == DefaultUsername && password == DefaultPassword);
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
