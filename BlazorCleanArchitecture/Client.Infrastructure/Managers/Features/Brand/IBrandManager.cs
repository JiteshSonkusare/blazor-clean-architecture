using Application.Features.Brands.Commands.Upsert;
using Application.Features.Brands.Queries.ResponseModel;
using BlazorCleanArchitecture.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Infrastructure.Managers.Features.Brand
{
    public interface IBrandManager : IManager
    {
        Task<IResult<List<GetBrandsViewModel>>> GetAllAsync();

        Task<IResult<GetBrandsViewModel>> GetByIdAsync(Guid id);

        Task<IResult<Guid>> SaveAsync(BrandUpsertCommand request);

        Task<IResult<Guid>> DeleteAsync(Guid id);
    }
}
