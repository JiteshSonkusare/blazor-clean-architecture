using Domain.Contracts;
using System;

namespace Domain.Entities
{
    public class Product : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
