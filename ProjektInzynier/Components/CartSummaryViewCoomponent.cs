using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektInzynier.Models;

namespace ProjektInzynier.Components
{
    //eksperyment z viewkomponentem w celu wyświetlania na stronie głównej ile materiałów jest już w koszyku.
    public class CartSummaryViewComponent : ViewComponent
    {
        private CartModel cart;

        public CartSummaryViewComponent(CartModel cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }

    }
}
