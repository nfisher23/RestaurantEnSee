using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public interface IMenuRepository
    {
        IQueryable<Menu> Menus { get; }
        IQueryable<MenuItem> MenuItems { get; }
        Menu GetFullMenuByName(string name);
        Photo GetPhotoByName(string fullName);
        MenuItem GetMenuItemById(int id);
    }
}
