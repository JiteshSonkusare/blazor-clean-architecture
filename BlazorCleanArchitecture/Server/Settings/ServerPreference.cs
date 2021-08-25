using BlazorCleanArchitecture.Shared.Constants.Localization;
using BlazorCleanArchitecture.Shared.Settings;
using System.Linq;

namespace Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

    }
}
