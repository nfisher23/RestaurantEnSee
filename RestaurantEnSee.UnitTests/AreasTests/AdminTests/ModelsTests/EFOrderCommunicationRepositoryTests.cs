using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NSubstitute;
using RestaurantEnSee.Areas.Home.Models;
using RestaurantEnSee.UnitTests.AreasTests.HomeTests.ModelsTests.seed;
using System;
using System.Collections.Generic;
using System.Text;
using RestaurantEnSee.Areas.Admin.Models;
using RestaurantEnSee.Areas.Admin.Models.Email;

namespace RestaurantEnSee.UnitTests.AreasTests.AdminTests.ModelsTests
{
    [TestFixture]
    public class EFOrderCommunicationRepositoryTests
    {
        public AppDbContext SharedDbContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAppDatabase2")
                .Options;

            SharedDbContext = new AppDbContext(options);
            SharedDbContext.Database.EnsureCreated();
            SharedDbContext.AdminEmails.Add(UnitTestSeedData.GenerateDefaultEmailConfig());
            SharedDbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            SharedDbContext.Database.EnsureDeleted();
        }

        [Test]
        public void DefaultEmailGetSet_UpdatesNew()
        {
            var repo = BasicEFOCRepoFactory();

            var newEmailConfig = new EmailConfiguration
            {
                EmailConfigurationId = 0,
                SmtpPassword = "pass",
                SmtpServer = "server",
                SmtpUsername = "user@name"
            };

            repo.DefaultEmailConfiguration = newEmailConfig;

            Assert.AreEqual(repo.DefaultEmailConfiguration, newEmailConfig);
        }

        [Test]
        public void DefaultEmailGetSet_GetsDefault()
        {
            var repo = BasicEFOCRepoFactory();

            var config = repo.DefaultEmailConfiguration;
            var seeded = UnitTestSeedData.GenerateDefaultEmailConfig();

            Assert.AreEqual(config.SmtpPassword, seeded.SmtpPassword);
            Assert.AreEqual(config.SmtpPort, seeded.SmtpPort);
            Assert.AreEqual(config.SmtpUsername, seeded.SmtpUsername);
        }


        private IOrderCommunicationRepository BasicEFOCRepoFactory()
        {
            return new EFOrderCommunicationRepository(SharedDbContext);
        }
        
    }
}
