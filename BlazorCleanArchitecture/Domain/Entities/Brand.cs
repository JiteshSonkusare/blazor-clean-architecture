using Domain.Contracts;
using System;

namespace Domain.Entities
{
    public class Brand : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
