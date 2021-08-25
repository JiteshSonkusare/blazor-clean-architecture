using Application.Interfaces.Repositories;
using AutoMapper;
using BlazorCleanArchitecture.Shared.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Upsert
{
    public class ProductUpsertCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public Guid BrandId { get; set; }
    }

    internal class ProductUpsertCommandHandler : IRequestHandler<ProductUpsertCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IStringLocalizer<ProductUpsertCommandHandler> _localizer;

        public ProductUpsertCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, IStringLocalizer<ProductUpsertCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(ProductUpsertCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Product>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.Barcode == command.Barcode, cancellationToken))
            {
                return await Result<Guid>.FailAsync(_localizer["Barcode already exists."]);
            }

            if (command.Id == Guid.Empty)
            {
                var product = _mapper.Map<Product>(command);
                await _unitOfWork.Repository<Product>().AddAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Saved"]);
            }
            else
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);
                if (product != null)
                {
                    product.Name = command.Name ?? product.Name;
                    product.Description = command.Description ?? product.Description;
                    product.Rate = (command.Rate == 0) ? product.Rate : command.Rate;
                    product.BrandId = (command.BrandId == Guid.Empty) ? product.BrandId : command.BrandId;
                    await _unitOfWork.Repository<Product>().UpdateAsync(product);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Updated"]);
                }
                else
                {
                    return await Result<Guid>.FailAsync(_localizer["Product Not Found!"]);
                }
            }
        }
    }
}
