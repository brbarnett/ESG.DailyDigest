namespace ESG.DailyDigest.Repositories.Interfaces
{
    public interface IEmailRepository
    {
        void SendEmail(string to, string from, string fromDisplayName, string subject, string body);
    }
}
