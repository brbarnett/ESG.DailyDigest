using System;
using ESG.DailyDigest.Repositories.Interfaces;
using ESG.DailyDigest.Services.Interfaces;

namespace ESG.DailyDigest.Services.Implementations
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;

        public TimeService(ITimeRepository timeRepository)
        {
            if (ReferenceEquals(timeRepository, null)) throw new ArgumentNullException("timeRepository");

            this._timeRepository = timeRepository;
        }

        public DateTime GetCurrentDateTime()
        {
            return _timeRepository.GetCurrentDateTime();
        }
    }
}
