using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantEnSee.Areas.Admin.Models.Email
{
    public class EmailConfiguration
    {
        // We do the constructors like this so EF Core doesn't throw a fit
        public EmailConfiguration() : this (587)
        { }

        public EmailConfiguration(int smtpPort)
        {
            SmtpPort = smtpPort;
        }

        public int EmailConfigurationId { get; set; }

        public string SmtpServer { get; set; }

        public int SmtpPort { get; }

        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

    }
}
