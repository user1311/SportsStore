using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext repository;

        public EFProductRepository(ApplicationDbContext repo)
        {
            repository = repo;
        }
        public IQueryable<Product> Products => repository.Products;

        public IEnumerable<Category> Categories => repository.Categories;

        public void Add(Product product) {
            repository.Products.Add(product);
            repository.SaveChanges();
        }

        public void SaveProduct(Product product)
        {
            Product p = repository.Products.FirstOrDefault(pro => pro.ProductID == product.ProductID);
            if (p != null)
            {
                p.ProductName = product.ProductName;
                p.QuantityPerUnit = product.QuantityPerUnit;
                p.CategoryID = product.CategoryID;
                p.Discounted = product.Discounted;
                p.UnitsInStock = product.UnitsInStock;
                p.UnitPrice = product.UnitPrice;
              
            }
            repository.Update(p);
            repository.SaveChanges();
        }
    }
}
