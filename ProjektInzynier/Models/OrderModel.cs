using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProjektInzynier.Models
{
    public class OrderModel
    {
        [BindNever]
        public int ID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Proszę podać imię i nazwisko")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać pierwszy wiersz adresu firmy")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Proszę podać miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę podać województwo")]
        public string State { get; set; }

        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Proszę podać kraj")]
        public string Country { get; set; }
        
    }
    
}
