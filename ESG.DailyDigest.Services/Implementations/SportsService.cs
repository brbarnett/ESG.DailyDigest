using System;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Repositories.Interfaces;
using ESG.DailyDigest.Services.Interfaces;

namespace ESG.DailyDigest.Services.Implementations
{
    public class SportsService : ISportsService
    {
        private readonly ISportDataRepository _sportDataRepository;

        public SportsService(ISportDataRepository sportDataRepository)
        {
            if (ReferenceEquals(sportDataRepository, null)) throw new ArgumentNullException("sportDataRepository");

            this._sportDataRepository = sportDataRepository;
        }

        public SportingEvent GetTodaysCubsGame()
        {
            return this._sportDataRepository.GetTodaysCubsGame();
        }
    }
}
