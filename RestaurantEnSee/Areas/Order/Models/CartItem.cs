using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal SubtotalBeforeTax
        {
            get
            {
                return (this.MenuItem.PriceBeforeTax * this.Quantity);
            }
        }
    }
}
