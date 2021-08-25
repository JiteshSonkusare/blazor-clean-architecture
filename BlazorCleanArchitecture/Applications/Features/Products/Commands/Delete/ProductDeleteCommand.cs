using Application.Interfaces.Repositories;
using BlazorCleanArchitecture.Shared.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Delete
{
    public class ProductDeleteCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IStringLocalizer<ProductDeleteCommandHandler> _localizer;

        public ProductDeleteCommandHandler(IUnitOfWork<Guid> unitOfWork, IStringLocalizer<ProductDeleteCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(ProductDeleteCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);
            if (product != null)
            {
                await _unitOfWork.Repository<Product>().DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Deleted"]);
            }
            else
            {
                return await Result<Guid>.FailAsync(_localizer["Product Not Found!"]);
            }
        }
    }
}
