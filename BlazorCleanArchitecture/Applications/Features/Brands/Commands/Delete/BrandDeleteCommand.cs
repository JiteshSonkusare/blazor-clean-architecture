using Application.Interfaces.Repositories;
using BlazorCleanArchitecture.Shared.Constants.Application;
using BlazorCleanArchitecture.Shared.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Delete
{
    public class BrandDeleteCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    internal class BrandDeleteCommandHandler : IRequestHandler<BrandDeleteCommand, Result<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<BrandDeleteCommandHandler> _localizer;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public BrandDeleteCommandHandler(IUnitOfWork<Guid> unitOfWork, IProductRepository productRepository, IStringLocalizer<BrandDeleteCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(BrandDeleteCommand command, CancellationToken cancellationToken)
        {
            var isBrandUsed = await _productRepository.IsBrandUsed(command.Id);
            if (!isBrandUsed)
            {
                var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(command.Id);
                if (brand != null)
                {
                    await _unitOfWork.Repository<Brand>().DeleteAsync(brand);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                    return await Result<Guid>.SuccessAsync(brand.Id, _localizer["Brand Deleted"]);
                }
                else
                {
                    return await Result<Guid>.FailAsync(_localizer["Brand Not Found!"]);
                }
            }
            else
            {
                return await Result<Guid>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}
