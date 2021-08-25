using BlazorCleanArchitecture.Shared.Settings;
using System.Threading.Tasks;
using BlazorCleanArchitecture.Shared.Wrapper;

namespace BlazorCleanArchitecture.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}