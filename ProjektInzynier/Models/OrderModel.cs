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

        [Required(ErrorMessage = "Proszę podać budowę !")]
        public string Construction { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę inwestora !")]
        public string Investor { get; set; }

        [Required(ErrorMessage = "Proszę podać nadzór")]
        public string Supervision { get; set; }

        [Required(ErrorMessage = "Proszę podać wykonawcę")]
        public string Contractor { get; set; }

        [Required(ErrorMessage = "Proszę podać projekanta branżowego")]
        public string IndustryEngineer { get; set; }
               
        
    }
    
}
