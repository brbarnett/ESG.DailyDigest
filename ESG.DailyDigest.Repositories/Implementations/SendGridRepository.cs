using System.Configuration;
using System.Net;
using System.Net.Mail;
using ESG.DailyDigest.Infrastructure;
using ESG.DailyDigest.Repositories.Interfaces;
using SendGrid;

namespace ESG.DailyDigest.Repositories.Implementations
{
    public class SendGridRepository : IEmailRepository
    {
        public void SendEmail(string to, string from, string fromDisplayName, string subject, string body)
        {
            SendGridMessage email = new SendGridMessage();
            string[] tos = to.Split(';');
            foreach (string address in tos)
            {
                email.AddTo(address.ToLower().Trim());
            }
            email.From = new MailAddress(from, fromDisplayName);
            email.Subject = subject;
            email.Text = body;
            email.Html = body;

            // true indicates that links in plain text portions of the email 
            // should also be overwritten for link tracking purposes. 
            email.EnableClickTracking(false);

            string username = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Services.Email.Credentials.Username];
            string password = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Services.Email.Credentials.Password];
            var credentials = new NetworkCredential(username, password);
            var transportWeb = new Web(credentials);

            transportWeb.Deliver(email);
        }
    }
}
