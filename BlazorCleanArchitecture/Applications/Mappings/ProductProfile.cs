using Application.Features.Products.Commands.Upsert;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductUpsertCommand, Product>().ReverseMap();
        }
    }
}
