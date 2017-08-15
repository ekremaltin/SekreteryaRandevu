using SekreteryaRandevu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SekreteryaRandevu.Controllers
{
    public class KisiController : Controller
    {
        private SekreteryaEntities db = new SekreteryaEntities();
        // GET: Kişi Listesi
        public ActionResult Liste()
        {
            var kisiListe = db.kisis.ToList();
            return View(kisiListe.ToList());
        }

        // GET: Kişi Detaylı Bilgi
        public ActionResult Detay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Liste");
            }
            var detayKisi = db.kisis.Where(k => k.kisiID == id).SingleOrDefault();            
            if(detayKisi==null)
            {
                return RedirectToAction("Liste");
            }
            return View(detayKisi);
        }

        // GET: Kisi/Create
        public ActionResult Olustur()
        {
            ViewBag.sirketler = db.sirkets.ToList();
            ViewBag.birimler = db.birims.ToList();
            ViewBag.iletisimTipler = db.iletisims.ToList();
            return View();
        }

        // POST: Kisi/Create
        [HttpPost]
        public ActionResult Olustur(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kisi/Edit/5
        public ActionResult Duzenle(int id)
        {
            return View();
        }

        // POST: Kisi/Edit/5
        [HttpPost]
        public ActionResult Duzenle(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kisi/Delete/5
        public ActionResult Sil(int id)
        {
            return View();
        }

        // POST: Kisi/Delete/5
        [HttpPost]
        public ActionResult Sil(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
