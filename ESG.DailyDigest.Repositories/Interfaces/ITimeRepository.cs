using System;

namespace ESG.DailyDigest.Repositories.Interfaces
{
    public interface ITimeRepository
    {
        DateTime GetCurrentDateTime();
    }
}
