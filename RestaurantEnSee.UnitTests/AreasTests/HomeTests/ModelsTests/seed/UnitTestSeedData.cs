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
        public static byte[] defPhotoBytes;
        public static Menu CreateDevelopmentMenu()
        {
            Menu m = new Menu
            {
                Categories = GenerateDevelopmentCategories(),
                MenuName = DefaultMenuName
            };

            return m;
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

            return p;
        }

        public static byte[] GetDevelopmentPhotoContent(string path)
        {
            defPhotoBytes = System.IO.File.ReadAllBytes(path);
            return defPhotoBytes; 
        }
    }
}
