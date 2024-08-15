using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using workshop.Data;
using workshop.Entities;
using workshop.Interfaces;

namespace workshop.Services
{
    public class ProductService : IProductService
    {
        public DatabaseContext DatabaseContext { get; set; }

        public ProductService(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;

        }

        public async Task<IEnumerable<Product>> FindAll()
        {
           var products = await this.DatabaseContext.Products.Include(p => p.Category).ToListAsync();
           return products;
        }

        public  Task<Product> FindById(int id)
        {
            return this.DatabaseContext.Products
            .Include(p => p.Category)
            .Where(p => p.ProductId == id).FirstOrDefaultAsync();
        }

        public Task Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Product product)
        {
            throw new NotImplementedException();
        }




        public Task<IEnumerable<Product>> Search(string name)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}