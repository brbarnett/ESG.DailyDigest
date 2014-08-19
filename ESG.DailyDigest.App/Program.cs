using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ESG.DailyDigest.DI;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Infrastructure;
using ESG.DailyDigest.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace ESG.DailyDigest.App
{
    static class Program
    {
        private static IEmailService _emailService;
        private static ISportsService _sportsService;
        private static IWeatherService _weatherService;

        static void Main(string[] args)
        {
            RegisterServices();

            List<EmailItemRow> emailItems = new List<EmailItemRow>();
            
            // create cubs row
            SportingEvent cubsGame = _sportsService.GetTodaysCubsGame();
            EmailItemRow cubsGameRow = new EmailItemRow();
            cubsGameRow.Icon = cubsGame.LogoImageUrl;
            cubsGameRow.Text = cubsGame.IsValid ? string.Format("Cubs game today at {0} {1} {2}",
                cubsGame.DateTime.ToString("hh:mm tt"),
                cubsGame.IsHomeGame ? "vs" : "@",
                cubsGame.Opponent) : "No data";
            emailItems.Add(cubsGameRow);

            // create weather row
            WeatherForecast weatherForecast = _weatherService.GetTodaysWeatherForecast();
            EmailItemRow weatherForecastRow = new EmailItemRow();
            weatherForecastRow.Icon = weatherForecast.ForecastIcon;
            weatherForecastRow.Text = weatherForecast.IsValid ? weatherForecast.ForecastText : "No data";
            emailItems.Add(weatherForecastRow);

            // initialize email
            string to = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Email.To];
            string from = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Email.From];
            string fromDisplayName = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Email.FromDisplayName];
            string html = GetEmailTemplate("EmailTemplates/Base.html");

            foreach (EmailItemRow emailItem in emailItems)
            {
                string rowHtml = GetEmailTemplate("EmailTemplates/ItemRow.html");
                rowHtml = rowHtml.ReplaceEmailVariable("##ICON##", emailItem.Icon);
                rowHtml = rowHtml.ReplaceEmailVariable("##TEXT##", emailItem.Text);

                emailItem.Html = rowHtml;
            }

            html = html.ReplaceEmailVariable("##ITEMROWS##",
                String.Join(
                string.Empty, 
                emailItems
                    .Select(item => item.Html)
                    .ToArray()));

            _emailService.SendEmail(
                to, 
                from, 
                fromDisplayName, 
                "ESG - Daily Digest Email", 
                html);

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey(false);
        }

        private static void RegisterServices()
        {
            _emailService = UnityContainerFactory.GetContainer().Resolve<IEmailService>();
            _sportsService = UnityContainerFactory.GetContainer().Resolve<ISportsService>();
            _weatherService = UnityContainerFactory.GetContainer().Resolve<IWeatherService>();
        }

        private static string GetEmailTemplate(string path)
        {
            string html = string.Empty;
            try
            {
                html = File.ReadAllText(path);
            }
            catch (Exception)
            {
                
            }
            
            return html;
        }

        private static string ReplaceEmailVariable(this string html, string name, string value)
        {
            return html.Replace(name, value);
        }
    }
}
