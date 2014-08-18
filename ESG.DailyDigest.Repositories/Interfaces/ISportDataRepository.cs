using ESG.DailyDigest.Domain.Models;

namespace ESG.DailyDigest.Repositories.Interfaces
{
    public interface ISportDataRepository
    {
        SportingEvent GetTodaysCubsGame();
    }
}
