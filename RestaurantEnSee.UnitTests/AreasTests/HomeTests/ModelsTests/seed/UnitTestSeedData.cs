using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RestaurantEnSee.UnitTests.AreasTests.HomeTests.ModelsTests.seed
{

    public static class UnitTestSeedData
    {
        public static string DefaultMenuName = "TestSeedDataMenu";
        public static List<string> photoNames = new List<string>();
        public static Photo defaultSeedPhoto = new Photo();
        public static Menu CreateDevelopmentMenu(int i = 1)
        {
            if (i == 1)
            {
                Menu m = new Menu
                {
                    Categories = GenerateDevelopmentCategories(),
                    MenuName = DefaultMenuName,
                    IsActiveMenu = true
                 };
                return m;
            }

            Menu m2 = new Menu
            {
                Categories = GenerateDevelopmentCategories(),
                MenuName = DefaultMenuName + i.ToString(),
                IsActiveMenu = false
            };
            return m2;

        }

        public static List<FoodCategory> GenerateDevelopmentCategories()
        {
            List<FoodCategory> cats = new List<FoodCategory>();

            cats.Add(new FoodCategory
            {
                Title = "Sandwiches",
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
                            Picture = GetDevelopmentPhoto()
                        });
                }
            }

            return cats;
        }


        public static Photo GetDevelopmentPhoto()
        {
            if (defaultSeedPhoto != null &&
                defaultSeedPhoto.Content != null &&
                defaultSeedPhoto.Content.Length > 10)
            {
                return defaultSeedPhoto;
            }


            string name = "food-outline";
            string ext = ".jpg";
            var path = Path.Combine(@"C:\Users\Nick\Documents\Visual Studio 2017\Projects\Web Applications\RestaurantEnSee\RestaurantEnSee.UnitTests\AreasTests\HomeTests\ModelsTests\seed\", name + ext);
            Photo p = new Photo
            {
                FullTitle = name + ".jpg",
                Content = GetDevelopmentPhotoContent(path)
            };

            if (!photoNames.Contains(p.FullTitle))
                photoNames.Add(p.FullTitle);

            defaultSeedPhoto = p;
            return defaultSeedPhoto;
        }

        public static byte[] GetDevelopmentPhotoContent(string path)
        {
            var developmentPhoto = System.IO.File.ReadAllBytes(path);
            return developmentPhoto;
        }
    }
}
