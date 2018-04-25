using System;
using System.Collections.Generic;
using System.Text;
using RestaurantEnSee.Areas.Home.Models;
using NUnit.Framework;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using System.IO;
using RestaurantEnSee.UnitTests.AreasTests.HomeTests.ModelsTests.seed;

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
            Assert.AreEqual(photo.Content, UnitTestSeedData.defPhotoBytes);
        }

        [Test]
        public void GetPhotoByName_PhotoDNE_ReturnsNull()
        {
            var repo = BasicEFMenuRepoFactory();

            var photo = repo.GetPhotoByName("FakePhotoName.jpg");

            Assert.IsNull(photo);
        }


        private IMenuRepository BasicEFMenuRepoFactory()
        {
            return new EFMenuRepository(SharedDbContext);
        }
        
    }


}
