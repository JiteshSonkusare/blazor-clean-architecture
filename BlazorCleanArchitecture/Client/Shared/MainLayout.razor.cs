using BlazorCleanArchitecture.Shared.Constants.Localization;
using Client.Infrastructure.Settings;
using MudBlazor;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        private MudTheme _currentTheme;
        private bool _drawerOpen = true;
        private string _language;

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = CleanFavTheme.DefaultTheme;
            _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
            
            var preferences = await _clientPreferenceManager.GetPreference();
            
            foreach (var language in LocalizationConstants.SupportedLanguages
                .Where(language => language.Code.Equals(preferences.LanguageCode)))
            { _language = language.DisplayName; }
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        public void Dispose()
        {
            //_ = hubConnection.DisposeAsync();
        }

        //private HubConnection hubConnection;
        //public bool IsConnected => hubConnection.State == HubConnectionState.Connected;
    }
}
