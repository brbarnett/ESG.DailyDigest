namespace ESG.DailyDigest.Infrastructure
{
    public static class Constants
    {
        public static class AppSettingKeys
        {
            public const string CubsScheduleUrl = "CubsScheduleUrl";
            public const string CubsLogoImageUrl = "CubsLogoImageUrl";

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
