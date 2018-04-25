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

        public Menu GetFullMenuByName(string name)
        {
            return ApplicationContext.Menus.Where(m => m.MenuName == name)
                .Include(m => m.Categories)
                .ThenInclude(c => c.FoodItems)
                .ThenInclude(f => f.Picture)
                .FirstOrDefault();
        }

        public Photo GetPhotoByName(string fullName)
        {
            return ApplicationContext.Photos.Where(p => p.FullTitle == fullName).FirstOrDefault();
        }
    }
}
