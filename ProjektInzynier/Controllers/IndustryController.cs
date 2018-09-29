using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektInzynier.Models;

namespace ProjektInzynier.Controllers
{
    [Authorize]
    public class IndustryController : Controller
    {
        private readonly EFCContext _context;

        public IndustryController(EFCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Industries.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industryModel = await _context.Industries
                .SingleOrDefaultAsync(m => m.ID == id);
            if (industryModel == null)
            {
                return NotFound();
            }

            return View(industryModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndustryModel industryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(industryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(industryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var post = _context.Industries.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IndustryModel industryModel)
        {
            if (id != industryModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Industries.Update(industryModel);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndustryModelExists(industryModel.ID))
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
            return View(industryModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var industryModel = _context.Industries.Find(id);
            if (industryModel == null)
            {
                return NotFound();
            }

            return View(industryModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var industryModel = await _context.Industries.SingleOrDefaultAsync(m => m.ID == id);
            _context.Industries.Remove(industryModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool IndustryModelExists(int id)
        {
            return _context.Industries.Any(e => e.ID == id);
        }
    }
}
