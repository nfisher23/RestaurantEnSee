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

        public Menu ActiveMenu
        {
            get
            {
                return ApplicationContext.Menus.Where(m => m.IsActiveMenu).FirstOrDefault();
            }
        }

        public void AddMenuItemToCategory(int foodCategoryId, int menuItemId)
        {
            var cat = ApplicationContext.FoodCategories.Where(c => c.FoodCategoryId == foodCategoryId)
                .Include(c => c.FoodItems).FirstOrDefault();

            if (cat != null)
            {
                var item = ApplicationContext.MenuItems.Where(m => m.MenuItemId == menuItemId).FirstOrDefault();
                if (item != null && !cat.FoodItems.Contains(item))
                {
                    cat.FoodItems.Add(item);
                    ApplicationContext.SaveChanges();
                }
            }
        }

        public void ChangeCategoryName(int id, string newName)
        {
            var cat = ApplicationContext.FoodCategories.Where(c => c.FoodCategoryId == id).FirstOrDefault();
            if (cat != null)
            {
                cat.Title = newName;
                ApplicationContext.SaveChanges();
            }
        }

        public int CreateNewFoodCategory(string categoryName, Menu menuToAssign)
        {
            var menu = GetFullMenuByName(menuToAssign.MenuName);
            menu.Categories.Add(new FoodCategory
            {
                Title = categoryName
            });
            ApplicationContext.SaveChanges();

            return GetFullMenuByName(menuToAssign.MenuName).Categories
                .Where(c => c.Title == categoryName).FirstOrDefault().FoodCategoryId;
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return ApplicationContext.MenuItems.Include(m => m.Picture).ToList();
        }

        public FoodCategory GetFullFoodCategoryById(int id)
        {
            return ApplicationContext.FoodCategories
                .Where(fc => fc.FoodCategoryId == id).FirstOrDefault();
        }

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

        public void RemoveFoodCategoryById(int id)
        {
            var cat = ApplicationContext.FoodCategories.Where(fc => fc.FoodCategoryId == id).FirstOrDefault();
            ApplicationContext.FoodCategories.Remove(cat);
            ApplicationContext.SaveChanges();
        }

        public void RemoveMenuItemFromCategory(int foodCategoryId, int menuItemId)
        {
            var cat = ApplicationContext.FoodCategories.Where(c => c.FoodCategoryId == foodCategoryId)
                .Include(c => c.FoodItems).FirstOrDefault();
            if (cat != null)
            {
                var item = ApplicationContext.MenuItems.Where(m => m.MenuItemId == menuItemId).FirstOrDefault();
                if (item != null && cat.FoodItems.Contains(item))
                {
                    cat.FoodItems.Remove(item);
                    ApplicationContext.SaveChanges();
                }
            }
        }

        public void SetActiveMenu(Menu newActiveMenu)
        {
            var menu = ApplicationContext.Menus.Where(m => m.IsActiveMenu).FirstOrDefault();
            menu.IsActiveMenu = false;

            var newMenu = ApplicationContext.Menus.Where(m => m.MenuName == newActiveMenu.MenuName).FirstOrDefault();
            newMenu.IsActiveMenu = true;
            ApplicationContext.SaveChanges();
        }

        public void UpdateMenuItem(MenuItem item)
        {
            if (item.MenuItemId > 0)
            {
                var dbItem = ApplicationContext.MenuItems
                    .Where(m => m.MenuItemId == item.MenuItemId).FirstOrDefault();

                if (dbItem != null)
                {
                    dbItem.Title = item.Title;
                    dbItem.Description = item.Description;
                    dbItem.PriceBeforeTax = item.PriceBeforeTax;
                    if (item.Picture != null)
                        dbItem.Picture = item.Picture;

                    ApplicationContext.SaveChanges();
                    return;
                }
            }
            else
            {
                ApplicationContext.MenuItems.Add(item);
                ApplicationContext.SaveChanges();
            }
        }
    }
}
