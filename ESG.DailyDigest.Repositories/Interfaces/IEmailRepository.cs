namespace ESG.DailyDigest.Repositories.Interfaces
{
    public interface IEmailRepository
    {
        bool SendEmail(string to, string from, string fromDisplayName, string subject, string body);
    }
}
