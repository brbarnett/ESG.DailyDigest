using ESG.DailyDigest.Domain.Models;

namespace ESG.DailyDigest.Services.Interfaces
{
    public interface IWeatherService
    {
        WeatherForecast GetTodaysWeatherForecast();
    }
}
