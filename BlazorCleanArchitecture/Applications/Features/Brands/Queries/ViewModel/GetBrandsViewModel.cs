using System;

namespace Application.Features.Brands.Queries.ResponseModel
{
    public class GetBrandsViewModel
    {
        public Guid Id             { get; set; }
        public string Name        { get; set; }
        public string Description { get; set; }
    }
}
