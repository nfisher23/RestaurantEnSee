using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantEnSee.Areas.Admin.Models.Email
{
    public class EmailConfiguration
    {
        public EmailConfiguration()
        { }

        public EmailConfiguration(int smtpPort)
        {
            SmtpPort = smtpPort;
        }

        public int EmailConfigurationId { get; set; }

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; } = 587;

        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
