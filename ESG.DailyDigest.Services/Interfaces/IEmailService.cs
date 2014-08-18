namespace ESG.DailyDigest.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string to, string from, string fromDisplayName, string subject, string body);
    }
}
