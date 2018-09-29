using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektInzynier.Interfaces;
using ProjektInzynier.Models;

namespace ProjektInzynier.Helpers
{
    public class EFProductRepository : IProductRepository
    {
        private EFCContext context;

        public EFProductRepository(EFCContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ProductModel> Products => context.Products;
    }
}
