using ESG.DailyDigest.Domain.Models;

namespace ESG.DailyDigest.Infrastructure
{
    public class Mapper
    {
        public static EmailItemRow Create(SportingEvent item)
        {
            EmailItemRow row = new EmailItemRow();
            row.Icon = item.LogoImageUrl;
            row.Text = item.IsValid ? string.Format("Cubs game today at {0} {1} {2}",
                item.DateTime.ToString("hh:mm tt"),
                item.IsHomeGame ? "vs" : "@",
                item.Opponent) : "No data";
            return row;
        }

        public static EmailItemRow Create(WeatherForecast item)
        {
            EmailItemRow row = new EmailItemRow();
            row.Icon = item.ForecastIcon;
            row.Text = item.IsValid ? item.ForecastText : "No data";
            return row;
        }

        public static EmailItemRow Create(TrainStatus item)
        {
            EmailItemRow row = new EmailItemRow();
            row.Icon = item.LogoUrl;
            row.Text = item.IsValid ? item.Table : "No data";
            return row;
        }
    }
}
