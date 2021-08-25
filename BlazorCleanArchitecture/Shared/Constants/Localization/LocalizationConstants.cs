namespace BlazorCleanArchitecture.Shared.Constants.Localization
{
    public static class LocalizationConstants
    {
        public static readonly LanguageCode[] SupportedLanguages = {
            new LanguageCode
            {
                Code = "en-US",
                DisplayName= "English"
            },
            new LanguageCode
            {
                Code = "no-NO",
                DisplayName = "Norsk"
            }
        };
    }
}
