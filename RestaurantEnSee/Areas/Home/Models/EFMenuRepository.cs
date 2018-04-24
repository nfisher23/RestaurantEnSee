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

        public Menu GetFullMenu(int id)
        {
            return ApplicationContext.Menus.Where(m => m.MenuId == id)
                .Include(m => m.Categories)
                .ThenInclude(c => c.FoodItems)
                .ThenInclude(f => f.Picture)
                .FirstOrDefault();
        }
    }
}
