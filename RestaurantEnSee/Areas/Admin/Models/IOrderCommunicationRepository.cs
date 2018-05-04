using RestaurantEnSee.Areas.Admin.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantEnSee.Areas.Admin.Models
{
    public interface IOrderCommunicationRepository
    {
        EmailConfiguration DefaultEmailConfiguration { get; set; }
    }
}
