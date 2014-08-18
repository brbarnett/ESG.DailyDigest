using System;
using System.Configuration;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Infrastructure;
using ESG.DailyDigest.Repositories.Interfaces;
using HtmlAgilityPack;

namespace ESG.DailyDigest.Repositories.Implementations
{
    public class EspnRepository : ISportDataRepository
    {

        private static string CubsScheduleUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettingKeys.CubsScheduleUrl]; }
        }

        private static string CubsLogoImageUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettingKeys.CubsLogoImageUrl]; }
        }

        public SportingEvent GetTodaysCubsGame()
        {
            SportingEvent cubsGame = new SportingEvent();
            cubsGame.LogoImageUrl = CubsLogoImageUrl;

            DateTime today = DateTime.Now;

            HtmlWeb client = new HtmlWeb();
            HtmlDocument document = client.Load(CubsScheduleUrl);

            if (!ReferenceEquals(document, null) && !ReferenceEquals(document.DocumentNode, null))
            {
                HtmlNode table = document.DocumentNode.SelectNode("//div[@id='my-teams-table']");
                if (!ReferenceEquals(table, null))
                {
                    HtmlNode dateElement = table.SelectNode(string.Format("//td//nobr[text()='{0}']", today.ToString("ddd, MMM dd")));
                    if (!ReferenceEquals(dateElement, null))
                    {
                        HtmlNode gameRowElement = dateElement.ParentNode.ParentNode;

                        string gameStatusString = gameRowElement.SelectNodeValue("td//li[@class='game-status']");
                        cubsGame.IsHomeGame = String.Equals(gameStatusString, "vs", StringComparison.InvariantCultureIgnoreCase);

                        cubsGame.Opponent = gameRowElement.SelectNodeValue("td//li[@class='team-name']//a");

                        HtmlNode timeElement = gameRowElement.SelectNode("td[3]//a");
                        if (ReferenceEquals(timeElement, null))
                        {
                            timeElement = gameRowElement.SelectNode("td[3]");
                        }

                        cubsGame.DateTime = DateTime.Parse(timeElement.InnerHtml).AddHours(-1);
                        cubsGame.IsValid = true;
                    }
                }
            }

            return cubsGame;
        }
    }
}
