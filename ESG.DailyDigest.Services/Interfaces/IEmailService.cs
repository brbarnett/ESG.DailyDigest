namespace ESG.DailyDigest.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string to, string from, string fromDisplayName, string subject, string body);
    }
}
