using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektInzynier.Models;

namespace ProjektInzynier
{
    public class EFCContext : IdentityDbContext
    {
        public DbSet<IndustryModel> Industries { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        //do connection stringa w startup
        public EFCContext(DbContextOptions opt) : base(opt)
        {

        }
    }
}
