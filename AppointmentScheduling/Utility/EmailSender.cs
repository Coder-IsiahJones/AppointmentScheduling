using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var api_key = _configuration.GetValue<string>("MailJet:Api_Key");
            var secret_Key = _configuration.GetValue<string>("MailJet:Secret_Key");

            MailjetClient client = new MailjetClient(api_key, secret_Key);
            {
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
           .Property(Send.FromEmail, "MailJet:Email")
           .Property(Send.FromName, "Appointment Scheduler")
           .Property(Send.Subject, subject)
           .Property(Send.HtmlPart, htmlMessage)
           .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", email}
                 }
               });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}