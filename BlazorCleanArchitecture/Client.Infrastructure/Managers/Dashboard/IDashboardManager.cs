using Client.Infrastructure.Managers;
using BlazorCleanArchitecture.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Dashboards.Queries.ViewModel;

namespace Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataViewModel>> GetDataAsync();
    }
}