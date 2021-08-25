using Application.Features.Products.Queries.ViewModel;
using Application.Interfaces.Repositories;
using AutoMapper;
using BlazorCleanArchitecture.Shared.Wrapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQuery : IRequest<Result<GetProductsViewModel>>
    {
        public Guid Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductsViewModel>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetProductsViewModel>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(query.Id);
            var mappedProduct = _mapper.Map<GetProductsViewModel>(product);
            return await Result<GetProductsViewModel>.SuccessAsync(mappedProduct);
        }
    }
}
