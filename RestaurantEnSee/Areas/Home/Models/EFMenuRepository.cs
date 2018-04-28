using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public class EFMenuRepository : IMenuRepository
    {
        private AppDbContext ApplicationContext;
        public EFMenuRepository(AppDbContext context)
        {
            ApplicationContext = context;
        }

        public IQueryable<Menu> Menus { get {
                return ApplicationContext.Menus;
            }
        }

        public IQueryable<MenuItem> MenuItems => ApplicationContext.MenuItems;

        public Menu GetFullMenuByName(string name)
        {
            return ApplicationContext.Menus.Where(m => m.MenuName == name)
                .Include(m => m.Categories)
                .ThenInclude(c => c.FoodItems)
                .ThenInclude(f => f.Picture)
                .FirstOrDefault();
        }

        public MenuItem GetMenuItemById(int id)
        {
            return ApplicationContext.MenuItems.Where(m => m.MenuItemId == id)
                .Include(m => m.Picture).FirstOrDefault();
        }

        public Photo GetPhotoByName(string fullName)
        {
            return ApplicationContext.Photos.Where(p => p.FullTitle == fullName).FirstOrDefault();
        }

        public void SetActiveMenu(Menu newActiveMenu)
        {
            var menu = ApplicationContext.Menus.Where(m => m.IsActiveMenu).FirstOrDefault();
            menu.IsActiveMenu = false;

            var newMenu = ApplicationContext.Menus.Where(m => m.MenuName == newActiveMenu.MenuName).FirstOrDefault();
            newMenu.IsActiveMenu = true;
            ApplicationContext.SaveChanges();
        }
    }
}
