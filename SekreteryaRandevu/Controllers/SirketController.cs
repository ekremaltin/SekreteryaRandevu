using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SekreteryaRandevu.Models;

namespace SekreteryaRandevu.Controllers
{
    public class SirketController : Controller
    {
        private SekreteryaEntities db = new SekreteryaEntities();

        // GET: Sirket
        public ActionResult Liste()
        {
            if (Session["id"] != null)
            {
                var sirkets = db.sirkets;
                return View(sirkets.ToList());
            }
            return RedirectToAction("Login", "users");
        }

        // GET: Sirket/Details/5
        public ActionResult Detay(int? id)
        {
            if (Session["id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sirket sirket = db.sirkets.Find(id);
                adre adres = db.adres.Where(m => m.adresID == sirket.sirketAdresID).SingleOrDefault();
                ViewBag.sirketAdres = adres;
                if (sirket == null)
                {
                    return HttpNotFound();
                }
                return View(sirket);
            }
            return RedirectToAction("Login", "users");

        }

        // GET: Sirket/Create
        public ActionResult Olustur()
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                ViewBag.ulkeler = new SelectList(db.ulkes, "ulkeID", "ulkeAdi");
                return View();
            }
            return RedirectToAction("Login", "users");

        }

        // POST: Sirket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olustur(sirket sirket)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (ModelState.IsValid)
                {
                    db.sirkets.Add(sirket);
                    db.SaveChanges();
                    return RedirectToAction("Liste");
                }

                ViewBag.sirketAdresID = new SelectList(db.adres, "adresID", "adresIlce", sirket.sirketAdresID);
                return View(sirket);
            }
            return RedirectToAction("Login", "users");

        }

        // GET: Sirket/Edit/5
        public ActionResult Duzenle(int? id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sirket sirket = db.sirkets.Find(id);
                if (sirket == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ulkeler = new SelectList(db.ulkes, "ulkeID", "ulkeAdi", sirket.adre.adresUlkeID);
                ViewBag.sehirler = new SelectList(db.sehirs, "sehirID", "sehirAdi", sirket.adre.adresILID);
                return View(sirket);
            }
            return RedirectToAction("Login", "users");

        }

        // POST: Sirket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(sirket sirket)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                for (int i = 0; i < sirket.iletisimToSirkets.Count(); i++)
                {
                    sirket.iletisimToSirkets.ToList()[i].sirketID = sirket.sirketID;

                }
                for (int i = 0; i < sirket.iletisimToSirkets.Count(); i++)
                {
                    db.Entry(sirket.iletisimToSirkets.ToList()[i]).State = EntityState.Modified;
                }
                if (sirket != null)
                {
                    db.Entry(sirket.adre).State = EntityState.Modified;
                    db.Entry(sirket).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Liste");
                }
                ViewBag.ulkeler = new SelectList(db.ulkes, "ulkeID", "ulkeAdi", sirket.adre.adresUlkeID);
                ViewBag.sehirler = new SelectList(db.sehirs, "sehirID", "sehirAdi", sirket.adre.adresILID);
                return View(sirket);
            }
            return RedirectToAction("Login", "users");

        }

        // GET: Sirket/Delete/5

        public ActionResult Sil(int? id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sirket sirket = db.sirkets.Find(id);
                adre adres = db.adres.Where(m => m.adresID == sirket.sirketAdresID).FirstOrDefault();
                List<iletisimToSirket> iletisimler = db.iletisimToSirkets.Where(m => m.sirketID == id).ToList();

                if (sirket == null || adres == null || iletisimler == null)
                {
                    return HttpNotFound();
                }
                for (int i = 0; i < iletisimler.Count(); i++)
                {
                    db.iletisimToSirkets.Remove(iletisimler[i]);
                }
                db.adres.Remove(adres);
                db.sirkets.Remove(sirket);
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
