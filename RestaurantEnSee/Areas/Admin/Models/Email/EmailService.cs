using System;
using System.Collections.Generic;
using System.Text;
using MailKit;
using MimeKit;
using System.Linq;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace RestaurantEnSee.Areas.Admin.Models.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _eConfig;

        public EmailService(EmailConfiguration config)
        {
            _eConfig = config;
        }

        public void Send(EmailMessage msg)
        {
            var message = new MimeMessage();
            message.To.AddRange(msg.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(msg.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = msg.Subject;

            message.Body = new TextPart("plain")
            {
                Text = msg.Content
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_eConfig.SmtpServer, _eConfig.SmtpPort, SecureSocketOptions.Auto);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_eConfig.SmtpUsername, _eConfig.SmtpPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
