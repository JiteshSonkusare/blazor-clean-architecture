using Application.Interfaces.Repositories;
using Domain.Entities;
using System;

namespace Infrastructure.Respositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, Guid> _repository;

        public BrandRepository(IRepositoryAsync<Brand, Guid> repository)
        {
            _repository = repository;
        }
    }
}
