using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektInzynier.Helpers;
using ProjektInzynier.Interfaces;
using ProjektInzynier.Models;

namespace ProjektInzynier.Controllers
{
    public class CartController : Controller
    {
        private readonly EFCContext _context;
        private IProductRepository repository;
        private CartModel cart;

                public CartController(EFCContext context, IProductRepository repo, CartModel cartService)
        {
            repository = repo;
            cart = cartService;
            _context = context;
                        
        }
        

        public ViewResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
       
        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            ProductModel product = _context.Products.Find(id);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            ProductModel product = _context.Products.FirstOrDefault(p => p.ID == id);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new {returnUrl});
        }
           
        
    }
}
