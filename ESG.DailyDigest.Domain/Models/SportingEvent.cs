using System;

namespace ESG.DailyDigest.Domain.Models
{
    public class SportingEvent : ServiceBase
    {
        public DateTime DateTime { get; set; }

        public string Opponent { get; set; }

        public string LogoImageUrl { get; set; }

        public bool IsHomeGame { get; set; }
    }
}
