using Application.Features.Brands.Queries.ResponseModel;
using Application.Interfaces.Repositories;
using AutoMapper;
using BlazorCleanArchitecture.Shared.Constants.Application;
using BlazorCleanArchitecture.Shared.Wrapper;
using Domain.Entities;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsQuery : IRequest<Result<List<GetBrandsViewModel>>>
    {
        public GetAllBrandsQuery() { }
    }

    internal class GetAllBrandsCachedQueryHandler : IRequestHandler<GetAllBrandsQuery, Result<List<GetBrandsViewModel>>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllBrandsCachedQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetBrandsViewModel>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Brand>>> getAllBrands = () => _unitOfWork.Repository<Brand>().GetAllAsync();
            var brandList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllBrandsCacheKey, getAllBrands);
            var mappedBrands = _mapper.Map<List<GetBrandsViewModel>>(brandList);
            return await Result<List<GetBrandsViewModel>>.SuccessAsync(mappedBrands);
        }
    }
}
