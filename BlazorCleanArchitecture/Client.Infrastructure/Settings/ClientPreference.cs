using System.Linq;
using BlazorCleanArchitecture.Shared.Constants.Localization;
using BlazorCleanArchitecture.Shared.Settings;

namespace Client.Infrastructure.Settings
{
    public record ClientPreference : IPreference
    {
        public bool IsRTL { get; set; }
        public bool IsDrawerOpen { get; set; }
        public string PrimaryColor { get; set; }
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";
    }
}