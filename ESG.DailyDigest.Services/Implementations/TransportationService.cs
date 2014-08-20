using System;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Repositories.Interfaces;
using ESG.DailyDigest.Services.Interfaces;

namespace ESG.DailyDigest.Services.Implementations
{
    public class TransportationService : ITransportationService
    {
        private readonly ITransportationRepository _transportationRepository;

        public TransportationService(ITransportationRepository transportationRepository)
        {
            if (ReferenceEquals(transportationRepository, null)) throw new ArgumentNullException("transportationRepository");

            this._transportationRepository = transportationRepository;
        }

        public TrainStatus GetTrainStatus()
        {
            return this._transportationRepository.GetTrainStatus();
        }
    }
}
