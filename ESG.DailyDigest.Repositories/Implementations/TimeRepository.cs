using System;
using ESG.DailyDigest.Repositories.Interfaces;

namespace ESG.DailyDigest.Repositories.Implementations
{
    public class TimeRepository : ITimeRepository
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
