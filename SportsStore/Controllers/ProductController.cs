using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private ICategoryRepository context;
        public int PageSize = 4;
        public ProductController(IProductRepository _repository, ICategoryRepository _context )
        {
            repository = _repository;
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult List(string categoryName="", int productPage = 1)
        {
            ProductListViewModel model= null;

            if (categoryName == "")
            {
                model = new ProductListViewModel
                {
                    Products = repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Count()
                    },
                    CurrentCategory = categoryName
                };
            }
            else
            {

                model = new ProductListViewModel
                {
                    Products = repository.Products
                .Where(p => p.Category.CategoryName == Uri.EscapeDataString(categoryName))
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Where(p => p.Category.CategoryName == categoryName).Count()
                    },
                    CurrentCategory = categoryName
                };


            }

            ViewBag.SelectedCategory = model.CurrentCategory;
            return View(model);
        }
            
                
    }
}