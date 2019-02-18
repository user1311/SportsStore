using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_NewLines() {

            Product p1 = new Product { ProductID = 1, ProductName = "P1", UnitPrice = 10.0m };
            Product p2 = new Product { ProductID = 2, ProductName = "P2", UnitPrice = 15.0m };
            Product p3 = new Product { ProductID = 1, ProductName = "P3", UnitPrice = 112.0m };
            Product p4 = new Product { ProductID = 1, ProductName = "P4", UnitPrice = 16.0m };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);

            Assert.Equal(35, cart.ComputeTotalValue());


        }

        [Fact] 
        public void Can_Add_Quantity_ForExistingLine()
        {

            Product p1 = new Product { ProductID = 1, ProductName = "P1", UnitPrice = 10.0m };
            Product p2 = new Product { ProductID = 2, ProductName = "P2", UnitPrice = 15.0m };
            Product p3 = new Product { ProductID = 1, ProductName = "P3", UnitPrice = 112.0m };
            Product p4 = new Product { ProductID = 1, ProductName = "P4", UnitPrice = 16.0m };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            Assert.Equal(55, cart.ComputeTotalValue());
        }

        [Fact]
        public void Can_Remove_Cartline()
        {

            Product p1 = new Product { ProductID = 1, ProductName = "P1", UnitPrice = 10.0m };
            Product p2 = new Product { ProductID = 2, ProductName = "P2", UnitPrice = 15.0m };
            Product p3 = new Product { ProductID = 1, ProductName = "P3", UnitPrice = 112.0m };
            Product p4 = new Product { ProductID = 1, ProductName = "P4", UnitPrice = 16.0m };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            cart.RemoveLine(p1);
            Assert.True(cart.CartLines.Where(c => c.Product.ProductID == p1.ProductID).Count()==0);
        }
        [Fact]
        public void Can_Remove_All()
        {
            Product p1 = new Product { ProductID = 1, ProductName = "P1", UnitPrice = 10.0m };
            Product p2 = new Product { ProductID = 2, ProductName = "P2", UnitPrice = 15.0m };
            Product p3 = new Product { ProductID = 1, ProductName = "P3", UnitPrice = 112.0m };
            Product p4 = new Product { ProductID = 1, ProductName = "P4", UnitPrice = 16.0m };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            cart.Clear();

            Assert.Empty(cart.CartLines);

        }


        [Fact]
        public void Can_Calculate_Totals()
        {
            Product p1 = new Product { ProductID = 1, ProductName = "P1", UnitPrice = 10.0m };
            Product p2 = new Product { ProductID = 2, ProductName = "P2", UnitPrice = 10.0m };
            Product p3 = new Product { ProductID = 3, ProductName = "P3", UnitPrice = 100.0m };
            Product p4 = new Product { ProductID = 4, ProductName = "P4", UnitPrice = 10.0m };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            decimal total = cart.ComputeTotalValue();

            Assert.Equal(50, total);
        }
    }
}
