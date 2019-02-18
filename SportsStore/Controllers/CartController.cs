using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private Cart cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            _repository = repository;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int ProductId,string ReturnUrl, int Quantity=1)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == ProductId);

            if(product != null)
            {
                cart.AddItem(product, Quantity);
            }
            return RedirectToAction("Index",new { ReturnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId, string ReturnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p=>p.ProductID==productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }

        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //    return cart;
        //}


        //private void SaveCart(Cart cart)
        //{
        //     HttpContext.Session.SetJson("Cart", cart);
        //}
    }
}