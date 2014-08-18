using System.Configuration;
using System.IO;
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

        static void Main(string[] args)
        {
            RegisterServices();

            SportingEvent cubsGame = _sportsService.GetTodaysCubsGame();

            if (cubsGame.IsValid && cubsGame.IsHomeGame)
            {
                string to = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Email.To];
                string from = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Email.From];
                string fromDisplayName = ConfigurationManager.AppSettings[Constants.AppSettingKeys.Email.FromDisplayName];
                string html = GetEmailTemplate();

                html = html.ReplaceEmailVariable("##CUBSLOGO##", cubsGame.LogoImageUrl);
                html = html.ReplaceEmailVariable("##CUBSTEXT##",
                    string.Format("Cubs game today at {0} {1} {2}", 
                        cubsGame.DateTime.ToString("hh:mm tt"),
                        cubsGame.IsHomeGame ? "vs" : "@",
                        cubsGame.Opponent));

                _emailService.SendEmail(
                    to, 
                    from, 
                    fromDisplayName, 
                    "ESG - Daily Digest Email", 
                    html);
            }

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey(false);
        }

        private static void RegisterServices()
        {
            _emailService = UnityContainerFactory.GetContainer().Resolve<IEmailService>();
            _sportsService = UnityContainerFactory.GetContainer().Resolve<ISportsService>();
        }

        private static string GetEmailTemplate()
        {
            string html = string.Empty;
            try
            {
                html = File.ReadAllText("EmailTemplate.html");
            }
            catch
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
