using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workshop.Entities;

namespace workshop.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> FindAll();
        Task<Product> FindById(int id);
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);
        Task<IEnumerable<Product>> Search(string name);

    }
}