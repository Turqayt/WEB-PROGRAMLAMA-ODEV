using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndirimKuponum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndirimKuponum.Controllers
{
    [Authorize]
    public class YorumController : Controller
    {
        private IndirimlerContext db = new IndirimlerContext();


        // GET: YorumController
        public ActionResult Index()
        {
            var yorum = db.Yorumlar
                               .Select(i =>
                               new YorumModel()
                               {
                                   KullaniciAdi = i.KullaniciAdi,
                                   YorumText = i.YorumText,
                                   YorumId=i.YorumId
                                   
                               }
                               );
            return View(yorum.ToList());
           
        }

        public ActionResult List()
        {
            var yorum = db.Yorumlar
                               .Select(i =>
                               new YorumModel()
                               {
                                   KullaniciAdi = i.KullaniciAdi,
                                   YorumText = i.YorumText,
                                   YorumId = i.YorumId

                               }
                               );
            return View(yorum.ToList());


        }

        // GET: YorumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: YorumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YorumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: YorumController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: YorumController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: YorumController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: YorumController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
