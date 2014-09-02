using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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
        private static ITransportationService _transportationService;
        private static IWeatherService _weatherService;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Registering services");
                RegisterServices();

                List<EmailItemRow> emailItems = new List<EmailItemRow>();

                // create cubs row
                Console.WriteLine("Adding Cubs row");
                SportingEvent cubsGame = _sportsService.GetTodaysCubsGame();
                emailItems.Add(Mapper.Create(cubsGame));

                // create weather row
                Console.WriteLine("Adding weather forecast row");
                WeatherForecast weatherForecast = _weatherService.GetTodaysWeatherForecast();
                emailItems.Add(Mapper.Create(weatherForecast));

                // create train status row
                Console.WriteLine("Adding CTA status row");
                TrainStatus trainStatus = _transportationService.GetTrainStatus();
                emailItems.Add(Mapper.Create(trainStatus));

                // initialize email
                Console.WriteLine("Initializing email");
                string to = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Services.Email.To];
                string from = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Services.Email.From];
                string fromDisplayName =
                    ConfigurationManager.AppSettings[Constants.AppSettingKeys.Services.Email.FromDisplayName];
                string html = GetEmailTemplate("EmailTemplates/Base.html");

                Console.WriteLine("Replacing email template variables (rows)");
                foreach (EmailItemRow emailItem in emailItems)
                {
                    string rowHtml = GetEmailTemplate("EmailTemplates/ItemRow.html");
                    rowHtml = rowHtml.Replace("##ICON##", emailItem.Icon);
                    rowHtml = rowHtml.Replace("##TEXT##", emailItem.Text);

                    emailItem.Html = rowHtml;
                }

                html = html.Replace("##ITEMROWS##",
                    String.Join(
                        string.Empty,
                        emailItems
                            .Select(item => item.Html)
                            .ToArray()));

                Console.WriteLine("Sending email");
                _emailService.SendEmail(
                    to,
                    from,
                    fromDisplayName,
                    "ESG - Daily Digest Email",
                    html);

                Console.WriteLine("Program finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Data);
            }
        }

        private static void RegisterServices()
        {
            _emailService = UnityContainerFactory.GetContainer().Resolve<IEmailService>();
            _sportsService = UnityContainerFactory.GetContainer().Resolve<ISportsService>();
            _transportationService = UnityContainerFactory.GetContainer().Resolve<ITransportationService>();
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
    }
}
