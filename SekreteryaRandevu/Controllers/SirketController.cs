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
            var sirkets = db.sirkets.Include(s => s.adre);
            return View(sirkets.ToList());
        }

        // GET: Sirket/Details/5
        public ActionResult Detay(int? id)
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
            return View(sirket);
        }

        // GET: Sirket/Create
        public ActionResult Olustur()
        {
            ViewBag.sirketAdresID = new SelectList(db.adres, "adresID", "adresIlce");
            return View();
        }

        // POST: Sirket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olustur([Bind(Include = "sirketID,sirketAdi,sirketSektor,sirketAdresID,sirketSorumluAdiSoyadi,sirketSorumluTel")] sirket sirket)
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

        // GET: Sirket/Edit/5
        public ActionResult Duzenle(int? id)
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
            ViewBag.sirketAdresID = new SelectList(db.adres, "adresID", "adresIlce", sirket.sirketAdresID);
            return View(sirket);
        }

        // POST: Sirket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle([Bind(Include = "sirketID,sirketAdi,sirketSektor,sirketAdresID,sirketSorumluAdiSoyadi,sirketSorumluTel")] sirket sirket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sirket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Liste");
            }
            ViewBag.sirketAdresID = new SelectList(db.adres, "adresID", "adresIlce", sirket.sirketAdresID);
            return View(sirket);
        }

        // GET: Sirket/Delete/5
        public ActionResult Sil(int? id)
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
            return View(sirket);
        }

        // POST: Sirket/Delete/5
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public ActionResult SilConfirmed(int id)
        {
            sirket sirket = db.sirkets.Find(id);
            db.sirkets.Remove(sirket);
            db.SaveChanges();
            return RedirectToAction("Liste");
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
