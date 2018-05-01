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
        Menu ActiveMenu { get; }
        List<MenuItem> GetAllMenuItems();

        FoodCategory GetFullFoodCategoryById(int id);
        void RemoveFoodCategoryById(int id);
        int CreateNewFoodCategory(string categoryName, Menu menuToAssign);
        void ChangeCategoryName(int id, string newName);

        void AddMenuItemToCategory(int foodCategoryId, int menuItemId);
        void RemoveMenuItemFromCategory(int foodCategoryId, int menuItemId);
        void UpdateMenuItem(MenuItem item);

        Photo GetPhotoByName(string fullName);


    }
}
