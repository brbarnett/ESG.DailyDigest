using System.Configuration;
using System.Net;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Infrastructure;
using ESG.DailyDigest.Repositories.Interfaces;
using Newtonsoft.Json;

namespace ESG.DailyDigest.Repositories.Implementations
{
    public class WundergroundRepository : IWeatherRepository
    {
        private static string WeatherUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettingKeys.WeatherUrl]; }
        }

        public WeatherForecast GetTodaysWeatherForecast()
        {
            WeatherForecast weatherForecast = new WeatherForecast();

            using (WebClient client = new WebClient())
            {
                string json = new WebClient().DownloadString(WeatherUrl);
                dynamic o = JsonConvert.DeserializeObject<dynamic>(json);

                if (!ReferenceEquals(o.forecast, null))
                {
                    if (!ReferenceEquals(o.forecast.txt_forecast, null))
                    {
                        if (!ReferenceEquals(o.forecast.txt_forecast.forecastday, null))
                        {
                            var day = o.forecast.txt_forecast.forecastday[0];
                            weatherForecast.ForecastIcon = day.icon_url.Value;
                            weatherForecast.ForecastText = day.fcttext.Value;

                            weatherForecast.IsValid = true;
                        }
                    }
                }
            }
            
            return weatherForecast;
        }
    }
}
