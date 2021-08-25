using Client.Infrastructure.Extensions;
using Client.Infrastructure.Managers.Dashboard;
using BlazorCleanArchitecture.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Features.Dashboards.Queries.ViewModel;

namespace Client.Infrastructure.Managers.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        public DashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataViewModel>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DashboardEndpoints.GetData);
            var data = await response.ToResult<DashboardDataViewModel>();
            return data;
        }
    }
}