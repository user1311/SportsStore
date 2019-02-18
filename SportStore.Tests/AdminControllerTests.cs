using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Contains_all_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                 new Product{ProductID=1,ProductName="P1"},
                    new Product{ProductID=1,ProductName="P2"},
                    new Product{ProductID=1,ProductName="P3"},
                    new Product{ProductID=1,ProductName="P4"},
                    new Product{ProductID=1,ProductName="P5"},
                    new Product{ProductID=1,ProductName="P6"}
            }).AsQueryable<Product>());

            AdminController target = new AdminController(mock.Object);

            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            // Assert 
            Assert.Equal(6,result.Count());

            Assert.True(result[0].ProductID == 1);
        }
        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }


    }
}
