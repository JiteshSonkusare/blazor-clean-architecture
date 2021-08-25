using Application.Features.Brands.Commands.Upsert;
using Application.Features.Brands.Queries.ResponseModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandUpsertCommand, Brand>().ReverseMap();
            CreateMap<GetBrandsViewModel, Brand>().ReverseMap();
        }
    }
}
