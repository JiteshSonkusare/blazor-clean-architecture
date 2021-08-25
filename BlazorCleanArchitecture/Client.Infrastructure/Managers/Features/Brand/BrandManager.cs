using Application.Features.Brands.Commands.Upsert;
using Application.Features.Brands.Queries.ResponseModel;
using BlazorCleanArchitecture.Shared.Wrapper;
using Client.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Infrastructure.Managers.Features.Brand
{
    public class BrandManager : IBrandManager
    {
        private readonly HttpClient _httpClient;

        public BrandManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.BrandEndpoints.Delete(id)}");
            return await response.ToResult<Guid>();
        }

        public async Task<IResult<List<GetBrandsViewModel>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.BrandEndpoints.GetAll);
            var data = await response.ToResult<List<GetBrandsViewModel>>();
            return await response.ToResult<List<GetBrandsViewModel>>();
        }

        public async Task<IResult<GetBrandsViewModel>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync(Routes.BrandEndpoints.GetById(id));
            return await response.ToResult<GetBrandsViewModel>();
        }

        public async Task<IResult<Guid>> SaveAsync(BrandUpsertCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.BrandEndpoints.Upsert, request);
            return await response.ToResult<Guid>();
        }
    }
}
