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
    public class IndirimlerController : Controller
    {
        private IndirimlerContext db = new IndirimlerContext();

        [Authorize]
        public ActionResult List(int? id, string AnahtarKelime)
        {
            var Indirim = db.Indirim
                .Where(i => i.Onay == true)
                .Select(i => new IndirimlerModel()
                {
                    Id = i.Id,
                    Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                    Aciklama = i.Aciklama,
                    EklenmeTarihi = i.EklenmeTarihi,
                    Anasayfa = i.Anasayfa,
                    Onay = i.Onay,
                    Resim = i.Resim,
                    KategoriId = i.KategoriId
                }
           ).AsQueryable();



            if (id != null)
            {
                Indirim = Indirim.Where(i => i.KategoriId == id);
            }
            return View(Indirim.ToList());


        }

        public ActionResult ListArama(int? id, string AnahtarKelime)
        {
            var Indirim = db.Indirim
                .Where(i => i.Onay == true)
                .Select(i => new IndirimlerModel()
                {
                    Id = i.Id,
                    Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                    Aciklama = i.Aciklama,
                    EklenmeTarihi = i.EklenmeTarihi,
                    Anasayfa = i.Anasayfa,
                    Onay = i.Onay,
                    Resim = i.Resim,
                    KategoriId = i.KategoriId
                }
           ).AsQueryable();

            if (string.IsNullOrEmpty("AnahtarKelime") == false)
            {
                Indirim = Indirim.Where(i => i.Baslik.Contains(AnahtarKelime) || i.Aciklama.Contains(AnahtarKelime));
            }

            return View(Indirim.ToList());

        }

        private readonly ApplicationDbContext _context;

        public IndirimlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Indirimler
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var indirim = db.Indirim.Include(i => i.Kategori).OrderByDescending(i => i.EklenmeTarihi);
            return View(indirim.ToList());
            //var applicationDbContext = _context.Indirimler.Include(i => i.Kategori);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Indirimler/Details/5
      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Indirimler indirimler = db.Indirim.Find(id);
            if (indirimler == null)
            {
                return NotFound();
            }
           
            return View(indirimler);
        }
        [Authorize(Roles = "Admin")]
        // GET: Indirimler/Create
        public IActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi");
            return View();
        }

        // POST: Indirimler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Baslik,Resim,Aciklama,Icerik,KategoriId")] Indirimler indirimler)
        {
            if (ModelState.IsValid)
            {
                indirimler.EklenmeTarihi = DateTime.Now;
                indirimler.Onay = false;
                indirimler.Anasayfa = false;

                db.Indirim.Add(indirimler);
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", indirimler.KategoriId);
            return View(indirimler);
        }

        // GET: Indirimler/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Indirimler indirimler = db.Indirim.Find(id);
            if (indirimler == null)
            {
                return NotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", indirimler.KategoriId);
            return View(indirimler);
        }

        // POST: Indirimler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Baslik,Resim,Aciklama,Icerik,Onay,Anasayfa,KategoriId")] Indirimler indirimler)
        {
            if (id != indirimler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 var entity = db.Indirim.Find(indirimler.Id);
                if (entity != null) {
                    entity.Baslik = indirimler.Baslik;
                    entity.Aciklama = indirimler.Aciklama;
                    entity.Resim = indirimler.Resim;
                    entity.Icerik = indirimler.Icerik;
                    entity.Onay = indirimler.Onay;
                    entity.Anasayfa = indirimler.Anasayfa;
                    entity.KategoriId = indirimler.KategoriId;

                    db.SaveChanges();

                    
                    return RedirectToAction("Index");
                }
               
                
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", indirimler.KategoriId);
            return View(indirimler);
        }

        // GET: Indirimler/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Indirimler indirimler = db.Indirim.Find(id);
            if (indirimler == null)
            {
                return NotFound();
            }

            return View(indirimler);
        }

        // POST: Indirimler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Indirimler indirimler = db.Indirim.Find(id);
            db.Indirim.Remove(indirimler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool IndirimlerExists(int id)
        {
            return _context.Indirimler.Any(e => e.Id == id);
        }

        public IActionResult YorumEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YorumEkle([Bind("KullaniciAdi,YorumText")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                
                yorum.IndirimId = 1;
                db.Yorumlar.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(yorum);
        }


    }
}
