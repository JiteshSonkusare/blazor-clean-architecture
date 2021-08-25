using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Respositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepositoryAsync<Product, Guid> _repository;

        public ProductRepository(IRepositoryAsync<Product, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsBrandUsed(Guid brandId)
        {
            return await _repository.Entities.AnyAsync(b => b.BrandId == brandId);
        }
    }
}
