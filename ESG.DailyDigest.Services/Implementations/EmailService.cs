using System;
using ESG.DailyDigest.Repositories.Interfaces;
using ESG.DailyDigest.Services.Interfaces;

namespace ESG.DailyDigest.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            if (ReferenceEquals(emailRepository, null)) throw new ArgumentNullException("emailRepository");

            this._emailRepository = emailRepository;
        }

        public void SendEmail(string to, string from, string fromDisplayName, string subject, string body)
        {
            _emailRepository.SendEmail(to, from, fromDisplayName, subject, body);
        }
    }
}
