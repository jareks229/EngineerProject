using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInzynier.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string TechnicalParameters { get; set; }
        public decimal Price { get; set; }
        public string Localization { get; set; }
        public string ProducentName { get; set; }
        public string ProducentAdress { get; set; }
        public IndustryModel IndustryModel { get; set; }
    }
}
