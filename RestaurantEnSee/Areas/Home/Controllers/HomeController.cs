using Microsoft.AspNetCore.Mvc;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        private IMenuRepository menuRepository;
        public HomeController(IMenuRepository repo)
        {
            menuRepository = repo;
        }

        public ViewResult Menu()
        {
            var name = menuRepository.Menus.FirstOrDefault().MenuName;
            var menu = menuRepository.GetFullMenuByName(name);
            return View(menu);
        }


        /// <summary>
        /// Gets image from the database
        /// </summary>
        /// <param name="fullImgTitle">Include the extension. E.g. 'pic.jpg', not 'pic'</param>
        /// <returns></returns>
        public FileResult GetImageFromDatabase([FromQuery]string fullImgTitle)
        {
            var photo = menuRepository.GetPhotoByName(fullImgTitle);
            if (photo != null)
            {
                return File(photo.Content, $"image/{photo.Extension}");
            }
            return null;
        }

    }
}
