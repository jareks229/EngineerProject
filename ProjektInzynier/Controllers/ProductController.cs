using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektInzynier.Models;

namespace ProjektInzynier.Controllers
{
    public class ProductController : Controller
    {
        private readonly EFCContext _context;

        public ProductController(EFCContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id, string producentName = "", int page = 1)
        {
            if (!String.IsNullOrWhiteSpace(producentName))
            {
                return View(await _context.Products.Where(q => q.ProducentName.StartsWith(producentName)).ToListAsync());
            }

            var list = _context.Products.ToList();

            var pageElements = 5;
            var pages = Math.Ceiling((decimal)list.Count() / pageElements);
            list = list.Skip(((page - 1) * pageElements)).Take(pageElements).ToList();

            ViewBag.Page = page;
            ViewBag.Pages = pages;

            return View(list);
            
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationModel = await _context.Products
                .SingleOrDefaultAsync(m => m.ID == id);
            if (consultationModel == null)
            {
                return NotFound();
            }

            return View(consultationModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .SingleOrDefaultAsync(m => m.ID == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .SingleOrDefaultAsync(m => m.ID == id);

            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductModel productModel)
        {
            if (id != productModel.ID)
            {
                return NotFound();
            }

            var productDb = await _context.Products
                .SingleOrDefaultAsync(m => m.ID == id);

            productDb.ProductName = productModel.ProductName;
            
            productDb.Localization = productModel.Localization;
            productDb.Price = productModel.Price;
            productDb.ProducentAdress = productModel.ProducentAdress;

            productDb.ProducentName = productModel.ProducentName;
            productDb.TechnicalParameters = productModel.TechnicalParameters;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productDb.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }
    }
}
