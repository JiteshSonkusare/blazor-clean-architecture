using Application.Features.Products.Queries.ViewModel;
using Application.Interfaces.Repositories;
using Application.Specifications.Features;
using BlazorCleanArchitecture.Shared.Wrapper;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Application.Extensions;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQuery : IRequest<PaginatedResult<GetProductsViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; }

        public GetAllProductsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResult<GetProductsViewModel>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork<Guid> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetProductsViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetProductsViewModel>> expression = e => new GetProductsViewModel
            {
                Id          = e.Id,
                Name        = e.Name,
                Description = e.Description,
                Rate        = e.Rate,
                Barcode     = e.Barcode,
                Brand       = e.Brand.Name,
                BrandId     = e.BrandId
            };
            var productFilterSpec = new ProductFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Product>().Entities
                   .Specify(productFilterSpec)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy);
                var data = await _unitOfWork.Repository<Product>().Entities
                   .Specify(productFilterSpec)
                   .OrderBy(ordering)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;

            }
        }
    }
}
