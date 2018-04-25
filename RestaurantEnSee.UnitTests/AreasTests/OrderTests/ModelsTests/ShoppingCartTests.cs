using NUnit.Framework;
using RestaurantEnSee.Areas.Home.Models;
using RestaurantEnSee.Areas.Order.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantEnSee.UnitTests.AreasTests.OrderTests.ModelsTests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        [Test]
        public void AddItem_AddsOne_Correct()
        {
            ShoppingCart c = new ShoppingCart();
            MenuItem m = new MenuItem
            {
                Description = "Some item",
                MenuItemId = 10,
                Title = "Some Title"
            };

            c.AddItem(m, 1);

            Assert.AreEqual(c.CartItems[0].MenuItem, m);
            Assert.AreEqual(c.CartItems[0].Quantity, 1);
        }

        [Test]
        public void AddItem_AddsMultiple_Correct()
        {
            ShoppingCart c = new ShoppingCart();
            MenuItem m = new MenuItem
            {
                Description = "Some item",
                MenuItemId = 10,
                Title = "Some Title"
            };

            c.AddItem(m, 3);
            Assert.AreEqual(c.CartItems[0].MenuItem, m);
            Assert.AreEqual(c.CartItems[0].Quantity, 3);
        }

        [Test]
        public void AddItem_AddMoreThanOne_AddsToExisting()
        {
            ShoppingCart c = new ShoppingCart();
            for (int i = 0; i < 3; i++)
            {
                var m = new MenuItem
                {
                    MenuItemId = (i + 1),
                    Description = $"Description #{i}",
                    Title = $"Title #{i}"
                };
                c.AddItem(m, 1);
            }

            Assert.AreEqual(c.CartItems.Count, 3);

            var newMenuItem = new MenuItem
            {
                Description = "Description #1",
                Title = $"Title #1",
                MenuItemId = 2
            };

            c.AddItem(newMenuItem, 3);
            var itemWeCareAbout = c.CartItems.Find(n => n.MenuItem.MenuItemId == newMenuItem.MenuItemId);

            Assert.IsNotNull(itemWeCareAbout);
            Assert.AreEqual(itemWeCareAbout.Quantity, 4);
        }

        [Test]
        public void RemoveItem_Removes()
        {
            ShoppingCart c = new ShoppingCart();
            MenuItem m = new MenuItem
            {
                Description = "Some item",
                MenuItemId = 10,
                Title = "Some Title"
            };

            c.AddItem(m, 3);
            var cartItem = c.CartItems[0];
            int countBeforeRemove = c.CartItems.Count;
            Assert.IsNotNull(cartItem);

            c.RemoveItem(cartItem);

            Assert.IsTrue(countBeforeRemove > c.CartItems.Count);
        }

    }
}
