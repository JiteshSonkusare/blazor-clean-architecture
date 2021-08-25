using Application.Features.Brands.Commands.Upsert;
using Application.Features.Brands.Queries.ResponseModel;
using BlazorCleanArchitecture.Shared.Constants.Application;
using Client.Extensions;
using Client.Infrastructure.Managers.Features.Brand;
using Client.Pages.Features.Modals;
using Client.Shared.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Pages.Features
{
    public partial class Brands
    {
        [Inject] private IBrandManager BrandManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetBrandsViewModel> _brandList = new();
        private GetBrandsViewModel _brand = new();
        private string _searchString = "";
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            await GetBrandsAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetBrandsAsync()
        {
            var response = await BrandManager.GetAllAsync();
            if (response.Succeeded)
            {
                _brandList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task Delete(Guid id)
        {
            var brand=_brandList.FirstOrDefault(b => b.Id.Equals(id));
            string deleteContent = $"{_localizer["Delete content "]}: {brand.Name}";
            var parameters = new DialogParameters
            {
                {nameof(DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await BrandManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task InvokeModal(Guid? id = null)
        {
            var parameters = new DialogParameters();
            if (id != Guid.Empty)
            {
                _brand = _brandList.FirstOrDefault(c => c.Id == id);
                if (_brand != null)
                {
                    parameters.Add(nameof(BrandUpsertModal.BrandUpsertCommandModel), new BrandUpsertCommand
                    {
                        Id = _brand.Id,
                        Name = _brand.Name,
                        Description = _brand.Description
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<BrandUpsertModal>(id == Guid.Empty ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _brand = new GetBrandsViewModel();
            await GetBrandsAsync();
        }

        private bool Search(GetBrandsViewModel brand)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (brand.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (brand.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}
