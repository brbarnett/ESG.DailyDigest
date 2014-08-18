using System;

namespace ESG.DailyDigest.Domain.Models
{
    public struct SportingEvent
    {
        public DateTime DateTime { get; set; }

        public string Opponent { get; set; }

        public string LogoImageUrl { get; set; }

        public bool IsHomeGame { get; set; }

        public bool IsValid { get; set; }
    }
}
