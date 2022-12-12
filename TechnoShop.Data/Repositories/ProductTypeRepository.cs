using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.ProductEntity;

namespace TechnoShop.Data.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(ProductType productType)
        {
            return _dbContext.ProductTypes.AddAsync(productType).AsTask();
        }

        public async Task Delete(string id)
        {
            var productType =await GetById(id);

            if (productType is null) return;

            _dbContext.Remove(productType);
        }

        public IQueryable<ProductType> GetAll()
        {
            return _dbContext.ProductTypes.AsQueryable();
        }

        public Task<ProductType> GetById(string id)
        {
            return _dbContext.ProductTypes.SingleAsync(q => q.ProductTypeId == id);
        }

        public Task<ProductType> GetByName(string name)
        {
            return _dbContext.ProductTypes.SingleAsync(q => q.TypeName == name);
        }

        public Task<bool> IsExists(string name)
        {
            return _dbContext.ProductTypes.AnyAsync(q => q.TypeName == name);
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Update(string id, ProductType productType)
        {
            _dbContext.Entry(productType).State = EntityState.Modified;
        }
    }
}
