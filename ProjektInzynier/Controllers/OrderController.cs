using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektInzynier.Models;

namespace ProjektInzynier.Controllers
{
    public class OrderController : Controller
    {
        //Dodaj widok !
        public ViewResult CheckOut() => View(new OrderModel());
    }
}
