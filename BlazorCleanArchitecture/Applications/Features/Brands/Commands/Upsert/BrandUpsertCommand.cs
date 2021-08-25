using Application.Interfaces.Repositories;
using AutoMapper;
using BlazorCleanArchitecture.Shared.Constants.Application;
using BlazorCleanArchitecture.Shared.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Upsert
{
    public class BrandUpsertCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    internal class BrandUpsertCommandHandler : IRequestHandler<BrandUpsertCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<BrandUpsertCommandHandler> _localizer;
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public BrandUpsertCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper, IStringLocalizer<BrandUpsertCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(BrandUpsertCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == Guid.Empty)
            {
                var brand = _mapper.Map<Brand>(command);
                await _unitOfWork.Repository<Brand>().AddAsync(brand);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                return await Result<Guid>.SuccessAsync(brand.Id, _localizer["Brand Saved"]);
            }
            else
            {
                var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(command.Id);
                if (brand != null)
                {
                    brand.Name = command.Name ?? brand.Name;
                    brand.Description = command.Description ?? brand.Description;
                    await _unitOfWork.Repository<Brand>().UpdateAsync(brand);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                    return await Result<Guid>.SuccessAsync(brand.Id, _localizer["Brand Updated"]);
                }
                else
                {
                    return await Result<Guid>.FailAsync(_localizer["Brand Not Found!"]);
                }
            }
        }
    }
}
