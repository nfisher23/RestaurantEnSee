using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Models
{
    public class ShoppingCart
    {
        public List<CartItem> CartItems => cartItems;

        private List<CartItem> cartItems = new List<CartItem>();

        public void AddItem(MenuItem itemToAdd, int quantity)
        {
            var cItem = cartItems.Where(n => n.MenuItem.MenuItemId == itemToAdd.MenuItemId)
                .FirstOrDefault();

            if (cItem == null)
            {
                cartItems.Add(new CartItem
                {
                    MenuItem = itemToAdd,
                    Quantity = quantity
                });
            }
            else
            {
                cItem.Quantity += quantity;
            }
        }

        public void RemoveItem(MenuItem item)
        {
            cartItems.RemoveAll(i => i.MenuItem.MenuItemId == item.MenuItemId);
        }
    }
}
