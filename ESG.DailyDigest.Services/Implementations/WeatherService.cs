using System;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Repositories.Interfaces;
using ESG.DailyDigest.Services.Interfaces;

namespace ESG.DailyDigest.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            if (ReferenceEquals(weatherRepository, null)) throw new ArgumentNullException("weatherRepository");

            this._weatherRepository = weatherRepository;
        }

        public WeatherForecast GetTodaysWeatherForecast()
        {
            return this._weatherRepository.GetTodaysWeatherForecast();
        }
    }
}
