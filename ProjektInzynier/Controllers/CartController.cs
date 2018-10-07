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
        /*
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.ProductModel.Price * item.Quantity);
            return View();
        }
        */

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

        /*
        public IActionResult Buy(int id)
        {
            ProductModel productModel = new ProductModel();
            if (SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart") == null)
            {
                List<ItemModel> cart = new List<ItemModel>();
                cart.Add(new ItemModel { ProductModel = _context.Products.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ItemModel> cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new ItemModel { ProductModel = _context.Products.Find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        */
        /*
        public IActionResult Remove(int id)
        {
            List<ItemModel> cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<ItemModel> cart = SessionHelper.GetObjectFromJson<List<ItemModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductModel.ID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        */
        
    }
}
