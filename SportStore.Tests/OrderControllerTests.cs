using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            Cart cart = new Cart();

            Order order = new Order();

            OrderController orderController = new OrderController(mock.Object, cart);

            ViewResult result = orderController.Checkout(order) as ViewResult;

            // Assert - check that the order hasn't been stored

            mock.Verify(m=> m.SaveOrder(It.IsAny<Order>()),Times.Never);

            // Assert - check that the method is returning the default view

            Assert.True(String.IsNullOrEmpty(result.ViewName));

            // Assert - check that I am passing invalid model

            Assert.False(result.ViewData.ModelState.IsValid);

        }


        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange 
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            Cart cart = new Cart();

            cart.AddItem(new Product(), 1);

            OrderController target = new OrderController(mock.Object, cart);

            target.ModelState.AddModelError("error", "error");

            ViewResult result = target.Checkout(new Order()) as ViewResult;

            //assert - check that no order is stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));

            Assert.False(result.ViewData.ModelState.IsValid);




        }

    }

}
