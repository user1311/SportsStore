using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void Add(Product product);
        IEnumerable<Category> Categories { get; }
        void SaveProduct(Product product);
    }
}
