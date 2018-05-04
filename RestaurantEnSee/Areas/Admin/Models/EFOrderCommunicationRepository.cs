using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantEnSee.Areas.Admin.Models.Email;
using RestaurantEnSee.Areas.Home.Models;

namespace RestaurantEnSee.Areas.Admin.Models
{
    public class EFOrderCommunicationRepository : IOrderCommunicationRepository
    {
        private AppDbContext ApplicationContext;

        public EFOrderCommunicationRepository(AppDbContext context)
        {
            ApplicationContext = context;
        }

        public EmailConfiguration DefaultEmailConfiguration
        {
            get
            {
                return ApplicationContext.AdminEmails.FirstOrDefault();
            }
            set
            {
                if (value.EmailConfigurationId == 0)
                {
                    var current = ApplicationContext.AdminEmails.FirstOrDefault();
                    ApplicationContext.AdminEmails.Remove(current);

                    // if we're going to persist this information, unfortunately we can't hash
                    // the password, since we need to use it to send any emails. security
                    // will have to be addressed in another way.
                    ApplicationContext.AdminEmails.Add(value);
                    ApplicationContext.SaveChanges();
                }
            }
        }
    }
}
