using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektInzynier.Models;

namespace ProjektInzynier.Controllers
{
    //kontroler do kończenia "zamówień" 
    public class OrderController : Controller
    {
        public ViewResult CheckOut() => View(new OrderModel());
    }
}
