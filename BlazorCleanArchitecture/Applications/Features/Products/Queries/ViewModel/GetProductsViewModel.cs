using System;

namespace Application.Features.Products.Queries.ViewModel
{
    public class GetProductsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public string Brand { get; set; }
        public Guid BrandId { get; set; }
    }
}
