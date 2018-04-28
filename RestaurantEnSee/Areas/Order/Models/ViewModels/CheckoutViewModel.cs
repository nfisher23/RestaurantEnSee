using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Order.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public ShoppingCart Cart { get; set; }

        [Display(Name = "Special Instructions", Description = "Enter special instructions for your order here")]
        public string SpecialInstructions { get; set; }

        [Required(ErrorMessage = "We need a name to identify this order")]
        [Display(Name = "Name for the order", Description = "Enter your name here")]
        public string NameOnOrder { get; set; }
    }
}
