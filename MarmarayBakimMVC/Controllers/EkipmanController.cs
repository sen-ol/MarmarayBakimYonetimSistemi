using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarmarayBakimMVC.Contexts;
using MarmarayBakimMVC.Models;

namespace MarmarayBakimMVC.Controllers
{
    public class EkipmanController : Controller
    {
        private readonly MarmarayBakimYonetimSistemiContext _context;

        public EkipmanController()
        {
             _context = new MarmarayBakimYonetimSistemiContext();
        }

        // GET: Ekipman
        public async Task<IActionResult> Index()
        {
            var marmarayBakimYonetimSistemiContext = _context.Ekipman.Include(e => e.EkipmanTur).Include(e => e.Istasyon).Include(e => e.Sistem);
            return View(await marmarayBakimYonetimSistemiContext.ToListAsync());
        }

        // GET: Ekipman/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ekipman == null)
            {
                return NotFound();
            }

            var ekipman = await _context.Ekipman
                .Include(e => e.EkipmanTur)
                .Include(e => e.Istasyon)
                .Include(e => e.Sistem)
                .FirstOrDefaultAsync(m => m.EkipmanId == id);
            if (ekipman == null)
            {
                return NotFound();
            }

            return View(ekipman);
        }


        public IActionResult bisey()
        {
            return View();
        }
        // GET: Ekipman/Create
        public IActionResult Create()
        {
            ViewData["EkipmanTurId"] = new SelectList(_context.EkipmanTur, "EkipmanTurId", "EkipmanTurAd");
            ViewData["IstasyonId"] = new SelectList(_context.Istasyon, "IstasyonId", "IstasyonAdi");
            ViewData["SistemId"] = new SelectList(_context.Sistem, "SistemId", "SistemAdi");
            return View();
        }

        // POST: Ekipman/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EkipmanId,EkipmanKod,IstasyonId,SistemId,EkipmanTurId")] Ekipman ekipman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ekipman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EkipmanTurId"] = new SelectList(_context.EkipmanTur, "EkipmanTurId", "EkipmanTurAd", ekipman.EkipmanTurId);
            ViewData["IstasyonId"] = new SelectList(_context.Istasyon, "IstasyonId", "IstasyonAdi", ekipman.IstasyonId);
            ViewData["SistemId"] = new SelectList(_context.Sistem, "SistemId", "SistemAdi", ekipman.SistemId);
            return View(ekipman);
        }

        // GET: Ekipman/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ekipman == null)
            {
                return NotFound();
            }

            var ekipman = await _context.Ekipman.FindAsync(id);
            if (ekipman == null)
            {
                return NotFound();
            }
            ViewData["EkipmanTurId"] = new SelectList(_context.EkipmanTur, "EkipmanTurId", "EkipmanTurAd", ekipman.EkipmanTurId);
            ViewData["IstasyonId"] = new SelectList(_context.Istasyon, "IstasyonId", "IstasyonAdi", ekipman.IstasyonId);
            ViewData["SistemId"] = new SelectList(_context.Sistem, "SistemId", "SistemAdi", ekipman.SistemId);
            return View(ekipman);
        }

        // POST: Ekipman/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EkipmanId,EkipmanKod,IstasyonId,SistemId,EkipmanTurId")] Ekipman ekipman)
        {
            if (id != ekipman.EkipmanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ekipman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EkipmanExists(ekipman.EkipmanId))
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
            ViewData["EkipmanTurId"] = new SelectList(_context.EkipmanTur, "EkipmanTurId", "EkipmanTurAd", ekipman.EkipmanTurId);
            ViewData["IstasyonId"] = new SelectList(_context.Istasyon, "IstasyonId", "IstasyonAdi", ekipman.IstasyonId);
            ViewData["SistemId"] = new SelectList(_context.Sistem, "SistemId", "SistemAdi", ekipman.SistemId);
            return View(ekipman);
        }

        // GET: Ekipman/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ekipman == null)
            {
                return NotFound();
            }

            var ekipman = await _context.Ekipman
                .Include(e => e.EkipmanTur)
                .Include(e => e.Istasyon)
                .Include(e => e.Sistem)
                .FirstOrDefaultAsync(m => m.EkipmanId == id);
            if (ekipman == null)
            {
                return NotFound();
            }

            return View(ekipman);
        }

        // POST: Ekipman/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ekipman == null)
            {
                return Problem("Entity set 'MarmarayBakimYonetimSistemiContext.Ekipman'  is null.");
            }
            var ekipman = await _context.Ekipman.FindAsync(id);
            if (ekipman != null)
            {
                _context.Ekipman.Remove(ekipman);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EkipmanExists(int id)
        {
          return _context.Ekipman.Any(e => e.EkipmanId == id);
        }
    }
}
