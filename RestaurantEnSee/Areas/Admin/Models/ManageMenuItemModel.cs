using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.Models
{
    public class ManageMenuItemModel
    {
        public ManageMenuItemModel()
        {
        }

        public MenuItem Item { get; set; }

        public IFormFile PhotoFile
        {
            get
            {
                return photoFile;
            }
            set
            {
                if (value.Length > 0)
                {
                    photoFile = value;
                    using (var ms = new MemoryStream())
                    {
                        value.CopyTo(ms);
                        Item.Picture = new Photo();
                        Item.Picture.Content = ms.ToArray();
                        Item.Picture.FullTitle = value.FileName;
                    }
                }
            }

        }
        private IFormFile photoFile;
    }
}
