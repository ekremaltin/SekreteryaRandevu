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
            if (detayKisi == null)
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
            ViewBag.adresUlkeID = new SelectList(db.ulkes, "ulkeID", "ulkeAdi");
            return View();
        }

        public JsonResult ilList(int id)
        {
            List<sehir> s = db.sehirs.Where(m => m.sehirUlkeID == id).OrderBy(m => m.sehirAdi).ToList();
            var secimList = new SelectList(s, "sehirID", "sehirAdi");
            return Json(secimList, JsonRequestBehavior.AllowGet);
        }

        // POST: Kisi/Create
        [HttpPost]
        public ActionResult Olustur(FormCollection collection, kisi kisi)
        {
            List<iletisimToKisi> i = new List<iletisimToKisi>();
            kisi k = new kisi();
            try
            {
                // TODO: Add insert logic here
                foreach (var item in collection.GetValues("cep"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 1;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("is"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 6;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("ev"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 4;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("mail"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 11;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("fax"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 9;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("site"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 12;
                    i.Add(data);
                }
                kisi.iletisimToKisis = i;               
                    db.kisis.Add(kisi);
                    db.SaveChanges();

                return RedirectToAction("Liste");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kisi/Edit/5
        public ActionResult Duzenle(int? id)
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
            ViewBag.kisiSirketID = new SelectList(db.sirkets, "sirketID", "sirketAdi", kisi.kisiSirketID);
            ViewBag.adresUlkeID = new SelectList(db.ulkes, "ulkeID", "ulkeAdi", kisi.adres.FirstOrDefault().adresUlkeID);
            return View(kisi);
        }

        // POST: Kisi/Edit/5
        //[HttpPost]
        //public ActionResult Duzenle(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        [HttpPost]
        public ActionResult Duzenle(int id, FormCollection collection, kisi kisi, adre adre)
        {
            if (kisi!=null&&adre !=null)
            {
                List<iletisimToKisi> i = new List<iletisimToKisi>();
                List<adre> ad = new List<adre>();
                ad.Add(adre);  // TODO: Add insert logic here
                foreach (var item in collection.GetValues("cep"))
                {


                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 1;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("is"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 6;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("ev"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 4;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("mail"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 11;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("fax"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 9;
                    i.Add(data);
                }
                foreach (var item in collection.GetValues("site"))
                {
                    iletisimToKisi data = new iletisimToKisi();
                    data.value = item.ToString();
                    data.iletisimID = 12;
                    i.Add(data);
                }
                kisi.iletisimToKisis = i;
                kisi.adres = ad;

                db.Entry(kisi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Liste");
            }
            ViewBag.kisiSirketID = new SelectList(db.sirkets, "sirketID", "sirketAdi", kisi.kisiSirketID);
            return View(kisi);
        }


        // GET: Kisi/Delete/5
        public ActionResult Sil(int id)
        {
            return View();
        }

        // POST: Kisi/Delete/5
        [HttpPost]
        public ActionResult Sil(int? id, FormCollection collection)
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
            return View(kisi);
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
