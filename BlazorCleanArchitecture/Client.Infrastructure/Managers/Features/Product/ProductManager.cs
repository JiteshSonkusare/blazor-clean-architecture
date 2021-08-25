using Application.Features.Products.Commands.Upsert;
using Application.Features.Products.Queries.ViewModel;
using BlazorCleanArchitecture.Shared.Wrapper;
using Client.Infrastructure.Extensions;
using Shared.Requests;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Infrastructure.Managers.Features.Product
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient _httpClient;

        public ProductManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<Guid>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.ProductEndpoints.Delete(id)}");
            return await response.ToResult<Guid>();
        }

        public async Task<PaginatedResult<GetProductsViewModel>> GetAllProductsAsync(PagingRequestViewModel request)
        {
            var response = await _httpClient.GetAsync(Routes.ProductEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetProductsViewModel>();
        }

        public async Task<IResult<GetProductsViewModel>> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync(Routes.ProductEndpoints.GetById(id));
            return await response.ToResult<GetProductsViewModel>();
        }

        public async Task<IResult<Guid>> UpsertAsync(ProductUpsertCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ProductEndpoints.Upsert, request);
            return await response.ToResult<Guid>();
        }
    }
}
