using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProjektInzynier.Models;

namespace ProjektInzynier.Helpers
{
    public class SessionCart : CartModel
    {
        public static CartModel GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("CartModel") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        public ISession Session { get; set; }

        public override void AddItem(ProductModel product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetObjectAsJson("CartModel", this);
        }

        public override void RemoveLine(ProductModel product)
        {
            base.RemoveLine(product);
            Session.SetObjectAsJson("CartModel", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("CartModel");
        }
    }
}
