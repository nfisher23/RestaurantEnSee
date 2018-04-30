using System;
using System.Collections.Generic;
using System.Text;
using RestaurantEnSee.Areas.Home.Models;
using NUnit.Framework;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using System.IO;
using RestaurantEnSee.UnitTests.AreasTests.HomeTests.ModelsTests.seed;
using System.Linq;

namespace RestaurantEnSee.UnitTests.AreasTests.HomeTests.ModelsTests
{
    [TestFixture]
    public class EFMenuRepositoryTests
    {
        private AppDbContext SharedDbContext;

        [SetUp]
        public void SetupDB()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAppDatabase")
                .Options;

            SharedDbContext = new AppDbContext(options);
            SharedDbContext.Database.EnsureCreated();
            SharedDbContext.Menus.Add(UnitTestSeedData.CreateDevelopmentMenu());
            SharedDbContext.Menus.Add(UnitTestSeedData.CreateDevelopmentMenu(2));
            SharedDbContext.SaveChanges();
        }

        [TearDown]
        public void TearDownDB()
        {
            SharedDbContext.Database.EnsureDeleted();
        }

        [Test]
        public void GetFullMenuById_IdExists_ReturnsNotNull()
        {
            var repo = BasicEFMenuRepoFactory();

            var menu = repo.GetFullMenuByName(UnitTestSeedData.DefaultMenuName);

            Assert.IsNotNull(menu);
        }

        [Test]
        public void GetFullMenuById_IdDoesNotExist_ReturnsNull()
        {
            var repo = BasicEFMenuRepoFactory();

            var menu = repo.GetFullMenuByName("NotARealName");

            Assert.IsNull(menu);
        }

        [Test]
        public void GetFullMenuById_IncludesEverything()
        {
            var repo = BasicEFMenuRepoFactory();

            var menu = repo.GetFullMenuByName(UnitTestSeedData.DefaultMenuName);

            Assert.IsNotNull(menu.Categories);
            Assert.IsTrue(menu.Categories.Count > 1);
            foreach (var cat in menu.Categories)
            {
                Assert.IsNotNull(cat.FoodItems);
                Assert.IsTrue(cat.FoodItems.Count > 1);

                foreach (var item in cat.FoodItems)
                {
                    Assert.IsNotNull(item.Picture);
                }
            }
        }

        [Test]
        public void GetPhotoByName_GetsBasicPhoto()
        {
            var repo = BasicEFMenuRepoFactory();

            var photo = repo.GetPhotoByName(UnitTestSeedData.photoNames[0]);

            Assert.IsNotNull(photo);
            Assert.AreEqual(photo.FullTitle, UnitTestSeedData.photoNames[0]);
            Assert.AreEqual(photo.Content, UnitTestSeedData.defaultSeedPhoto.Content);
        }

        [Test]
        public void GetPhotoByName_PhotoDNE_ReturnsNull()
        {
            var repo = BasicEFMenuRepoFactory();

            var photo = repo.GetPhotoByName("FakePhotoName.jpg");

            Assert.IsNull(photo);
        }

        [Test]
        public void GetMenuItemById_ItemExists_ReturnsCorrect()
        {
            var repo = BasicEFMenuRepoFactory();

            var menu = repo.GetFullMenuByName(repo.Menus.FirstOrDefault().MenuName);
            List<MenuItem> itemsToGet = new List<MenuItem>();

            foreach (var cat in menu.Categories)
            {
                foreach (var item in cat.FoodItems)
                {
                    itemsToGet.Add(item);
                }
            }

            Assert.IsTrue(itemsToGet.Count > 4);
            foreach (var menuItem in itemsToGet)
            {
                var repoMenuItem = repo.GetMenuItemById(menuItem.MenuItemId);

                Assert.IsNotNull(repoMenuItem);
                Assert.AreEqual(menuItem, repoMenuItem);
            }
        }

        [Test]
        public void GetMenuItemById_ItemDNE_ReturnsNull()
        {
            var repo = BasicEFMenuRepoFactory();

            var nonExistent1 = repo.GetMenuItemById(0);
            var nonExistent2 = repo.GetMenuItemById(-10);

            Assert.IsNull(nonExistent1);
            Assert.IsNull(nonExistent2);
        }

        [Test]
        public void GetMenuItemById_IncludesPhoto()
        {
            var repo = BasicEFMenuRepoFactory();
            var menuItemIdToGet = repo.MenuItems.FirstOrDefault().MenuItemId;

            var menuItemWithPic = repo.GetMenuItemById(menuItemIdToGet);

            Assert.IsNotNull(menuItemWithPic.Picture);
            Assert.IsTrue(menuItemWithPic.Picture.Content.Length > 100);
        }

        [Test]
        public void SetActiveMenu_UpdatesToNew()
        {
            var repo = BasicEFMenuRepoFactory();

            var menus = repo.Menus.ToList();
            Assert.AreEqual(menus.Count, 2); // data check

            Assert.IsTrue(menus[0].IsActiveMenu);
            repo.SetActiveMenu(menus[1]);
            Assert.IsTrue(menus[1].IsActiveMenu);
        }

        [Test]
        public void SetActiveMenu_DisablesOld()
        {
            var repo = BasicEFMenuRepoFactory();

            var menus = repo.Menus.ToList();
            Assert.AreEqual(menus.Count, 2); // data check

            Assert.IsTrue(menus[0].IsActiveMenu);
            repo.SetActiveMenu(menus[1]);
            Assert.IsFalse(menus[0].IsActiveMenu);
        }

        [Test]
        public void GetFoodCategoryById_GetsValidOnes()
        {
            var repo = BasicEFMenuRepoFactory();
            var menu = repo.GetFullMenuByName(repo.Menus.FirstOrDefault().MenuName);

            var categories = menu.Categories.ToList();

            Assert.IsTrue(categories.Count > 3);
            foreach (var cat in categories)
            {
                var repoCat = repo.GetFullFoodCategoryById(cat.FoodCategoryId);
                Assert.IsNotNull(repoCat);
                Assert.AreEqual(cat, repoCat);
            }
        }

        [Test]
        public void GetFoodCategoryById_Nonexistent_ReturnsNull()
        {
            var repo = BasicEFMenuRepoFactory();

            var categories = SharedDbContext.FoodCategories.ToList();
            var maxCat = categories.Max(c => c.FoodCategoryId);
            var minCat = categories.Min(c => c.FoodCategoryId);

            var tooHighId = repo.GetFullFoodCategoryById(maxCat + 1);
            var tooLowId = repo.GetFullFoodCategoryById(minCat - 1);

            Assert.IsNull(tooHighId);
            Assert.IsNull(tooLowId);
        }

        [Test]
        public void GetFoodCategoryById_IncludesFoodItems()
        {
            var repo = BasicEFMenuRepoFactory();

            var categories = SharedDbContext.FoodCategories.ToList();

            Assert.IsTrue(categories.Count > 4);
            foreach (var cat in categories)
            {
                var repoCat = repo.GetFullFoodCategoryById(cat.FoodCategoryId);
                Assert.IsNotNull(repoCat.FoodItems);
                Assert.IsTrue(repoCat.FoodItems.Count > 1);
                foreach (var item in repoCat.FoodItems)
                {
                    Assert.IsTrue(item.Title.Length > 1);
                    Assert.IsTrue(item.Description.Length > 1);
                }
            }
        }

        [Test]
        public void AddMenuItemToCategory_ItemDNE_AddsItem()
        {
            var repo = BasicEFMenuRepoFactory();

            var cat = SharedDbContext.FoodCategories.First();
            int numOfItemsBeforeAct = cat.FoodItems.Count;

            var itemToAdd = repo.MenuItems.Where(m => !cat.FoodItems.Contains(m)).First();

            repo.AddMenuItemToCategory(cat.FoodCategoryId, itemToAdd.MenuItemId);

            var catFromRepo = repo.GetFullFoodCategoryById(cat.FoodCategoryId);
            Assert.IsTrue(catFromRepo.FoodItems.Count > numOfItemsBeforeAct);

            Assert.Contains(itemToAdd, catFromRepo.FoodItems);
        }

        [Test]
        public void RemoveMenuItemToCategory_ItemExists_RemovesItem()
        {
            var repo = BasicEFMenuRepoFactory();

            var cat = SharedDbContext.FoodCategories.First();
            int numOfItemsBeforeAct = cat.FoodItems.Count;

            var itemToRemove = repo.MenuItems.Where(m => cat.FoodItems.Contains(m)).First();

            repo.RemoveMenuItemFromCategory(cat.FoodCategoryId, itemToRemove.MenuItemId);

            var catFromRepo = repo.GetFullFoodCategoryById(cat.FoodCategoryId);
            Assert.IsTrue(catFromRepo.FoodItems.Count < numOfItemsBeforeAct);

            Assert.IsFalse(catFromRepo.FoodItems.Contains(itemToRemove));
        }

        private IMenuRepository BasicEFMenuRepoFactory()
        {
            return new EFMenuRepository(SharedDbContext);
        }
        
    }


}
