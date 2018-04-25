using Microsoft.AspNetCore.Razor.TagHelpers;
using RestaurantEnSee.Areas.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Home.TagHelpers
{
    [HtmlTargetElement(Attributes = "image")]
    public class DbImageTagHelper : TagHelper
    {
        public Photo Image { get; set; }

        IMenuRepository menuRepository;
        public DbImageTagHelper(IMenuRepository repo)
        {
            menuRepository = repo;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src", "/dbimgs/" + Image.FullTitle);
            output.Attributes.SetAttribute("class", "img-thumbnail");
        }
    }
}
