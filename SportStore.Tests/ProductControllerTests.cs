using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SportsStore.Models.ViewModels;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Arrange 
            Mock<IProductRepository> mock =new Mock<IProductRepository>();

            mock.Setup(p => p.Products).Returns((
                new Product[] {
                    new Product{ProductID=1,ProductName="P1"},
                    new Product{ProductID=1,ProductName="P2"},
                    new Product{ProductID=1,ProductName="P3"},
                    new Product{ProductID=1,ProductName="P4"},
                    new Product{ProductID=1,ProductName="P5"},
                    new Product{ProductID=1,ProductName="P6"}
                }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object,null);
            controller.PageSize = 3;

            //Act 
            ProductListViewModel result =
                controller.List("", 2).ViewData.Model as ProductListViewModel;
            //Assert
            Product[] prodArray = result.Products.ToArray();

            Assert.True(prodArray.Length == 3);
            Assert.Equal("P4", prodArray[0].ProductName);
            Assert.Equal("P5", prodArray[1].ProductName);
        }


        [Fact]
        public void Can_Send_Pagination_ViewModel()
        {

            //Arrange

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns((new Product[] {
                new Product{ProductID=1,ProductName="P1"},
                new Product{ProductID=2,ProductName="P2"},
                new Product{ProductID=3,ProductName="P3"},
                new Product{ProductID=4,ProductName="P4"},
                new Product{ProductID=5,ProductName="P5"}
            }).AsQueryable<Product>());

            //Arrange
            ProductController controller = new ProductController(mock.Object,null) { PageSize = 3 };
            //ItemsPerPage == PageSize

            //Act
            ProductListViewModel result = controller.List("", 2).ViewData.Model as ProductListViewModel;

            //Assert
            PagingInfo info = result.PagingInfo;
            Assert.Equal(2, info.CurrentPage);
            Assert.Equal(3, info.ItemsPerPage);
            Assert.Equal(5, info.TotalItems);
            Assert.Equal(2, info.TotalPages);
        }


        [Fact]
        public void Can_Filter_Products()
        {

            Mock <IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns((new Product[] {
                new Product{ProductID=1,ProductName="P1",CategoryID=1},
                new Product{ProductID=2,ProductName="P2",CategoryID=1},
                new Product{ProductID=3,ProductName="P3",CategoryID=1},
                new Product{ProductID=4,ProductName="P4",CategoryID=1},
                new Product{ProductID=5,ProductName="P5",CategoryID=2},
                new Product{ProductID=6,ProductName="P6",CategoryID=3},
                new Product{ProductID=7,ProductName="P7",CategoryID=2},
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object,null);

            Product[] result = (controller.List("", 1).ViewData.Model as ProductListViewModel).Products.ToArray();

            Assert.Equal(4, result.Length);
            Assert.True(result[0].ProductName=="P1" && result[0].CategoryID==1);
                
        }

        [Fact]
        public void Can_Get_Categories()
        {
            Mock<ICategoryRepository> mockCategories = new Mock<ICategoryRepository>();
            mockCategories.Setup(c => c.Categories).Returns((new Category[] {
                new Category{CategoryID=1,CategoryName="C1"},
                new Category{CategoryID=2,CategoryName="C2"},
                new Category{CategoryID=3,CategoryName="C2"}
            }));

            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(p => p.Products).Returns((new Product[] {
                new Product{ProductID=1,ProductName="P1",CategoryID=1},
                new Product{ProductID=2,ProductName="P2",CategoryID=1},
                new Product{ProductID=3,ProductName="P3",CategoryID=1},
                new Product{ProductID=4,ProductName="P4",CategoryID=1},
                new Product{ProductID=5,ProductName="P5",CategoryID=2},
                new Product{ProductID=6,ProductName="P6",CategoryID=3},
                new Product{ProductID=7,ProductName="P7",CategoryID=2},
            }).AsQueryable<Product>());

            Mock<ApplicationDbContext> mockContext = new Mock<ApplicationDbContext>();

            ProductController controller = new ProductController(mockProducts.Object, mockCategories.Object);

            Product[] results = (controller.List("", 1).ViewData.Model as ProductListViewModel).Products.ToArray();

            Assert.True(results[0].CategoryID == 1);


        }
    }
}
