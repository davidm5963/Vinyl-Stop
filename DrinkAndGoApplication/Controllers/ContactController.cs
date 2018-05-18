using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using VinylStop.Services;
using Microsoft.Extensions.Options;
using VinylStop.ViewModels;

namespace VinylStop.Controllers
{
    public class ContactController : Controller
    {
        private EmailConfiguration _emailConfiguration;

        public ContactController(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public ViewResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(ContactViewModel contactViewModel)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("davidmoeller115963@gmail.com"));
            message.To.Add(new MailboxAddress("davidmoeller115963@gmail.com"));

            if (!ModelState.IsValid)
            {
                return View(contactViewModel);
            }
            else
            {
                message.Subject = contactViewModel.Subject;
                message.Body = new TextPart("html")
                {
                    Text = "From: " + contactViewModel.Name + "<br>" +
                            "Contact Information: " + contactViewModel.UserEmail + "<br>" +
                            "Message: " + contactViewModel.Message
                };

                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587);


                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate("davidmoeller115963@gmail.com", "Ryajus1010");

                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("ContactError");
                }

                return RedirectToAction("ContactSuccess");
            }
        }

        public ViewResult ContactError() =>  View();

        public ViewResult ContactSuccess() => View();


    }
}