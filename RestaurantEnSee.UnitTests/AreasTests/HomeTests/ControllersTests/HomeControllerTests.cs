using NSubstitute;
using NUnit.Framework;
using RestaurantEnSee.Areas.Home.Controllers;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantEnSee.UnitTests.AreasTests.HomeTests.ControllersTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void GetImageFromDatabase_CallsGetPhoto()
        {
            var mockRepo = Substitute.For<IMenuRepository>();
            string name = "fullTitle.jpg";
            var controller = new HomeController(mockRepo);

            var result = controller.GetImageFromDatabase(name);

            mockRepo.Received().GetPhotoByName(name);
        }
    }
}
