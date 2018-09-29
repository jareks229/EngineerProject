using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektInzynier.Models;

namespace ProjektInzynier.Interfaces
{
    public interface IProductRepository
    {   
        IQueryable<ProductModel> Products { get; }
    }
}
