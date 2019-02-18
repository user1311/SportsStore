using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }


        public IActionResult Index() => View(_repository.Products);

        [HttpGet]
        public IActionResult Create() => View(new ProductCategoryViewModel {
            
        });

        public IActionResult Edit(int productID)
        {
            dynamic MyModel = new ProductCategoryViewModel();
            MyModel.Product = _repository.Products.FirstOrDefault(p => p.ProductID == productID);
            MyModel.Categories = _repository.Categories.ToList();
            return View(MyModel);
        }

        [HttpPost]
        public IActionResult Edit([Bind("ProductID,CategoryID,ProductName,QuantityPerUnit,UnitPrice,UnitsInStock,Discounted")]Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                }
            return RedirectToAction(nameof(Index));
        }
    }
}