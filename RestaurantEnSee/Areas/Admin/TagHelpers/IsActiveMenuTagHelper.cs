using Microsoft.AspNetCore.Razor.TagHelpers;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.TagHelpers
{
    public class IsActiveMenuTagHelper : TagHelper
    {
        public Menu CurrentMenu { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add("name", "IsActiveMenu");
            output.Attributes.Add("type", "radio");
            output.Attributes.Add("value", CurrentMenu.MenuName);
            if (CurrentMenu.IsActiveMenu)
                output.Attributes.Add(new TagHelperAttribute("checked", null, HtmlAttributeValueStyle.Minimized));
        }
    }
}
