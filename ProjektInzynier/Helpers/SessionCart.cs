using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProjektInzynier.Models;

namespace ProjektInzynier.Helpers
{
    //do sesji
    public class SessionCart : CartModel
    {
        private IHttpContextAccessor _accessor;
        private string UserName;
        

        public SessionCart(IHttpContextAccessor httpContextaccessor)
        {
            _accessor = httpContextaccessor;
            UserName = _accessor.HttpContext.User.Identity.Name;
            Session = _accessor?.HttpContext.Session;
            var cartmodel = _accessor.HttpContext.Session?.GetJson<List<CartLine>>(UserName);
            if(cartmodel != null)
            {
                lineCollection = cartmodel;
            }
            
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(ProductModel product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson(UserName, this.lineCollection);
        }

        public override void RemoveLine(ProductModel product)
        {
            base.RemoveLine(product);
            Session.SetJson(UserName, this.lineCollection);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove(UserName);
        }
    }
}
