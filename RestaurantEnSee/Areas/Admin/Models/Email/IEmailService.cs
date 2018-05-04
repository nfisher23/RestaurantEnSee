using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantEnSee.Areas.Admin.Models.Email
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
