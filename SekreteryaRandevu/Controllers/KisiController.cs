﻿using SekreteryaRandevu.Models;
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
            return View();
        }

        // POST: Kisi/Create
        [HttpPost]
        public ActionResult Olustur(FormCollection collection, kisi kisi)
        {
            List<iletisimToKisi> i = new List<iletisimToKisi>();
            //iletisimToKisi data = new iletisimToKisi();
            kisi k = new kisi();

            k.kisiAdi = collection["kisiAdi"];
            //foreach (var item in collection["cep"])
            //{
            //    k.iletisimToKisis.Add(item);
            //}
            var a = collection.GetValues("cep");

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
