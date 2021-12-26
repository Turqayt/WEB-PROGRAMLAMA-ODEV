using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndirimKuponum.Data;
using IndirimKuponum.Models;
using Microsoft.AspNetCore.Authorization;

namespace IndirimKuponum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KategoriController : Controller
    {
        private IndirimlerContext db = new IndirimlerContext();

        private readonly ApplicationDbContext _context;


        public KategoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kategori
        public async Task<IActionResult> Index()
        {
            var kategoriler = db.Kategoriler
                               .Select(i =>
                               new KategoriModel()
                               {
                                   Id = i.Id,
                                   KategoriAdi = i.KategoriAdi,
                                   IndirimSayisi = i.Indirim.Count()
                               }
                               );
            return View(kategoriler.ToList());
        }

        // GET: Kategori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kategori kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Kategori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriAdi")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kategori kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,KategoriAdi")] Kategori kategori)
        {
           
            if (ModelState.IsValid)
            {
                var entity = db.Kategoriler.Find(kategori.Id);
                if (entity != null)
                {
                    entity.Id = kategori.Id;
                    entity.KategoriAdi = kategori.KategoriAdi;

                    db.SaveChanges();


                    return RedirectToAction("Index");
                }
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kategori kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Kategori kategori = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategori.Any(e => e.Id == id);
        }
    }
}
