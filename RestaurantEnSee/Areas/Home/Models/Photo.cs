using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.Models
{
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[RegularExpression(@"^*\.(jpeg|jpg|png)$")] // test later
        public string FullTitle { get; set; }
        public byte[] Content { get; set; }
        public string Extension {
            get {
                return FullTitle.Split('.').Last();
            }
        }
    }
}
