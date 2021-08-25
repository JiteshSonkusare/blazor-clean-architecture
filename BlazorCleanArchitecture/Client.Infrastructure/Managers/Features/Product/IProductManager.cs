using Application.Features.Products.Commands.Upsert;
using Application.Features.Products.Queries.ViewModel;
using BlazorCleanArchitecture.Shared.Wrapper;
using Shared.Requests;
using System;
using System.Threading.Tasks;

namespace Client.Infrastructure.Managers.Features.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetProductsViewModel>> GetAllProductsAsync(PagingRequestViewModel request);

        Task<IResult<GetProductsViewModel>> GetById(Guid id);

        Task<IResult<Guid>> UpsertAsync(ProductUpsertCommand request);

        Task<IResult<Guid>> DeleteAsync(Guid id);
    }
}
