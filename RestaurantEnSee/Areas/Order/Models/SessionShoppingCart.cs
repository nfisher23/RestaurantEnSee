using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestaurantEnSee.Areas.Home.Models;
using RestaurantEnSee.Areas.Order.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Models
{
    public class SessionShoppingCart : ShoppingCart
    {
        public static ShoppingCart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            SessionShoppingCart c = session?
                .GetJson<SessionShoppingCart>(DefaultCartKey) ?? new SessionShoppingCart();

            c.Session = session;
            return c;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(MenuItem itemToAdd, int quantity)
        {
            base.AddItem(itemToAdd, quantity);
            Session.SetJson(DefaultCartKey, this);
        }

        public override void RemoveItem(MenuItem item)
        {
            base.RemoveItem(item);
            Session.SetJson(DefaultCartKey, this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove(DefaultCartKey);
        }

    }
}
