using System;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Repositories.Interfaces;
using ESG.DailyDigest.Services.Interfaces;

namespace ESG.DailyDigest.Services.Implementations
{
    public class SportsService : ISportsService
    {
        private readonly ISportDataRepository _sportDataRepository;
        private readonly ITimeRepository _timeRepository;

        public SportsService(ISportDataRepository sportDataRepository, ITimeRepository timeRepository)
        {
            if (ReferenceEquals(sportDataRepository, null)) throw new ArgumentNullException("sportDataRepository");
            if (ReferenceEquals(timeRepository, null)) throw new ArgumentNullException("timeRepository");

            this._sportDataRepository = sportDataRepository;
            this._timeRepository = timeRepository;
        }

        public SportingEvent GetTodaysCubsGame()
        {
            DateTime now = this._timeRepository.GetCurrentDateTime();
            return this._sportDataRepository.GetTodaysCubsGame(now);
        }
    }
}
