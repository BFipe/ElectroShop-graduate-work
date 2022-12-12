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
    internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(Product product)
        {
            return _dbContext.Products.AddAsync(product).AsTask();
        }

        public async Task Delete(string id)
        {
            var product = await GetById(id);

            if (product is null) return;

            _dbContext.Remove(product);
        }

        public IQueryable<Product> GetAll()
        {
            return _dbContext.Products.AsQueryable();
        }

        public async Task<Product> GetById(string id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Update(string id, Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
        }

        public Task<bool> IsExists(string name)
        {
            return _dbContext.Products.AnyAsync(q => q.Name == name);
        }
    }
}
