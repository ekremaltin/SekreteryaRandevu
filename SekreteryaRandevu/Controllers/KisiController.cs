using SekreteryaRandevu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            if (Session["id"] != null)
            {
                var kisiListe = db.kisis.ToList();
                return View(kisiListe.ToList());
            }
            return RedirectToAction("Login", "users");
        }

        // GET: Kişi Detaylı Bilgi
        public ActionResult Detay(int? id)
        {
            if (Session["id"] != null)
            {
                if (id == null)
                {
                    return RedirectToAction("Liste");
                }
                var detayKisi = db.kisis.Where(k => k.kisiID == id).SingleOrDefault();
                if (detayKisi == null)
                {
                    return RedirectToAction("Liste");
                }
                return View(detayKisi);
            }
            return RedirectToAction("Login", "users");
        }

        // GET: Kisi/Create
        public ActionResult Olustur()
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                ViewBag.sirketler = db.sirkets.ToList();
                ViewBag.birimler = db.birims.ToList();
                ViewBag.iletisimTipler = db.iletisims.ToList();
                ViewBag.adresUlkeID = new SelectList(db.ulkes, "ulkeID", "ulkeAdi");
                return View();
            }
            return RedirectToAction("Login","users");
        }

        public JsonResult ilList(int id)
        {
            List<sehir> s = db.sehirs.Where(m => m.sehirUlkeID == id).OrderBy(m => m.sehirAdi).ToList();
            var secimList = new SelectList(s, "sehirID", "sehirAdi");
            return Json(secimList, JsonRequestBehavior.AllowGet);
        }

        // POST: Kisi/Create
        [HttpPost]
        public ActionResult Olustur(kisi kisi)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                kisi k = new kisi();
                try
                {
                    // TODO: Add insert logic here  
                    if (kisi != null)
                    {
                        db.kisis.Add(kisi);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Liste");
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "users");

        }

        // GET: Kisi/Edit/5
        public ActionResult Duzenle(int? id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                kisi kisi = db.kisis.Find(id);
                if (kisi == null)
                {
                    return HttpNotFound();
                }
                List<iletisimToKisi> kisiİletisim = db.iletisimToKisis.Where(m => m.kisiID == id).ToList();
                List<adre> kisiAdres = db.adres.Where(n => n.adresKisiID == id).ToList();
                ViewBag.sirketler = new SelectList(db.sirkets, "sirketID", "sirketAdi", kisi.kisiSirketID);
                ViewBag.birimler = new SelectList(db.birims, "birimID", "birimAdi", kisi.birimID);
                ViewBag.adresUlkeID = new SelectList(db.ulkes, "ulkeID", "ulkeAdi", kisi.adres.FirstOrDefault().adresUlkeID);
                ViewBag.sehirler = new SelectList(db.sehirs, "sehirID", "sehirAdi", kisi.adres.FirstOrDefault().adresUlkeID);
                return View(kisi);
            }
            return RedirectToAction("Login", "users");

        }
        [HttpPost]
        public ActionResult Duzenle(kisi kisi)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (kisi != null)
                {
                    for (int i = 0; i < kisi.adres.Count(); i++)
                    {
                        kisi.adres.ToList()[i].adresKisiID = kisi.kisiID;
                    }
                    for (int i = 0; i < kisi.iletisimToKisis.Count(); i++)
                    {
                        kisi.iletisimToKisis.ToList()[i].kisiID = kisi.kisiID;

                    }
                    for (int i = 0; i < kisi.adres.Count(); i++)
                    {
                        db.Entry(kisi.adres.ToList()[i]).State = EntityState.Modified;
                    }
                    for (int i = 0; i < kisi.iletisimToKisis.Count(); i++)
                    {
                        db.Entry(kisi.iletisimToKisis.ToList()[i]).State = EntityState.Modified;
                    }
                    db.Entry(kisi).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Liste");

                }
                ViewBag.kisiSirketID = new SelectList(db.sirkets, "sirketID", "sirketAdi", kisi.kisiSirketID);
                return View(kisi);
            }
            return RedirectToAction("Login", "users");

        }

        // POST: Kisi/Delete/5        
        public ActionResult SilIslem(int? id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                kisi kisi = db.kisis.Find(id);
                List<adre> adresler = db.adres.Where(m => m.adresKisiID == id).ToList();
                List<iletisimToKisi> iletisimler = db.iletisimToKisis.Where(m => m.kisiID == id).ToList();
                if (kisi == null)
                {
                    return HttpNotFound();
                }
                for (int i = 0; i < adresler.Count(); i++)
                {
                    db.adres.Remove(adresler[i]);
                }
                for (int i = 0; i < iletisimler.Count(); i++)
                {
                    db.iletisimToKisis.Remove(iletisimler[i]);
                }
                db.kisis.Remove(kisi);
                db.SaveChanges();
                return RedirectToAction("Liste");
            }
            return RedirectToAction("Login", "users");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
