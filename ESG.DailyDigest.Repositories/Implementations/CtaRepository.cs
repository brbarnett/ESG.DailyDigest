using System;
using System.Configuration;
using ESG.DailyDigest.Domain.Models;
using ESG.DailyDigest.Infrastructure;
using ESG.DailyDigest.Repositories.Interfaces;
using HtmlAgilityPack;

namespace ESG.DailyDigest.Repositories.Implementations
{
    public class CtaRepository : ITransportationRepository
    {
        private static string CtaStatusUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettingKeys.Services.Train.ServiceUrl]; }
        }

        private static string CtaLogoImageUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettingKeys.Services.Train.LogoUrl]; }
        }

        public TrainStatus GetTrainStatus()
        {
            TrainStatus trainStatus = new TrainStatus();
            trainStatus.LogoUrl = CtaLogoImageUrl;

            HtmlWeb client = new HtmlWeb();
            HtmlDocument document = client.Load(CtaStatusUrl);

            if (!ReferenceEquals(document, null) && !ReferenceEquals(document.DocumentNode, null))
            {
                HtmlNode table = document.DocumentNode.SelectNode("//table[@class='tblsystatus']");
                if (!ReferenceEquals(table, null))
                {
                    HtmlNodeCollection links = table.SelectNodes("tr//a");
                    foreach (HtmlNode link in links)
                    {
                        string relativeHref = link.Attributes["href"].Value;
                        link.SetAttributeValue("href", "http://www.transitchicago.com" + relativeHref);
                    }

                    trainStatus.Table = table.OuterHtml;
                    
                    trainStatus.IsValid = true;
                }
            }

            return trainStatus;
        }
    }
}
