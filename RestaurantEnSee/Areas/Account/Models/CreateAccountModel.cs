﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Account.Models
{
    public class CreateAccountModel : AccountLoginModel
    {
        [Compare("Password", ErrorMessage = "Your passwords must match")]
        [UIHint("password")]
        public string RetypePassword { get; set; }
    }
}
