namespace ESG.DailyDigest.Infrastructure
{
    public static class Constants
    {
        public static class AppSettingKeys
        {
            public static class Services
            {
                public static class Sports
                {
                    public const string CubsScheduleUrl = "CubsScheduleUrl";
                    public const string CubsLogoImageUrl = "CubsLogoImageUrl";
                }

                public static class Train
                {
                    public const string LogoUrl = "CtaLogoImageUrl";
                    public const string ServiceUrl = "CtaStatusUrl";
                }

                public static class Weather
                {
                    public const string ServiceUrl = "WeatherUrl";
                }
                public static class Email
                {
                    public const string To = "EmailTo";
                    public const string ToDisplayName = "EmailToDisplayName";
                    public const string From = "EmailFrom";
                    public const string FromDisplayName = "EmailFromDisplayName";

                    public static class Credentials
                    {
                        public const string Username = "EmailUsername";
                        public const string Password = "EmailPassword";
                    }
                }
            }
        }
    }
}
