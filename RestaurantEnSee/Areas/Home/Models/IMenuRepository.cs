using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public interface IMenuRepository
    {
        IQueryable<Menu> Menus { get; }
        Menu GetFullMenuByName(string name);

        IQueryable<MenuItem> MenuItems { get; }
        MenuItem GetMenuItemById(int id);
        void SetActiveMenu(Menu newActiveMenu);


        Photo GetPhotoByName(string fullName);

    }
}
