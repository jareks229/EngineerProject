using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInzynier.Models
{
    public class IndustryModel
    {
        public int ID { get; set; }
        public string IndustryName { get; set; }
        public ICollection<ProductModel> ProductModels { get; set; }

    }
}
