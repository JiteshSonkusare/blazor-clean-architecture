using MudBlazor;
using BlazorCleanArchitecture.Shared.Managers;
using System.Threading.Tasks;

namespace Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();
    }
}