using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart cart;

        public OrderController(IOrderRepository repo, Cart cartService)
        {
            _repository = repo;
            cart = cartService;
        }
        public IActionResult Checkout() => View(new Order());

        public IActionResult List() => View(_repository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        public IActionResult MarkShipped(int OrderID)
        {
            Order order = _repository.Orders
                .FirstOrDefault(o => o.OrderID == OrderID);
            if (order != null)
            {
                order.Shipped = true;
                _repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));

        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.CartLines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.CartLines.ToArray();
                _repository.SaveOrder(order);
                return RedirectToAction("Completed");
            }
            else
            {
                return View(order);
            }
        }

        public IActionResult Completed()
        {
            cart.Clear();
            return View();
        }

    }
}