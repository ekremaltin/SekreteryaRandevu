using SekreteryaRandevu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SekreteryaRandevu.Controllers
{
    public class PlanController : Controller
    {
        private SekreteryaEntities db = new SekreteryaEntities();
        // GET: Plan
        public ActionResult Home()
        {
            ViewBag.kisiListe = new SelectList(db.kisis, "kisiID", "kisiAdi");
            return View();
        }
        
        [HttpGet]
        public JsonResult GetEvents()
        {
            var plans = db.plans;
            List<planToKisi> k = new List<planToKisi>();
            var c = db.planToKisis.Select(m=> new getKatilimci {
                pkID=m.pkID,
                pkKisiID=m.pkKisiID,
                pkPlanID=m.pkPlanID,
                pkisSource=m.pkisSource
            });

            var a = db.plans.Select(m => new getPlan
            {
                planID = m.planID,
                planKisaBilgi = m.planKisaBilgi,
                planUzunBilgi = m.planUzunBilgi,
                planColor = m.planColor,
                planEndTarih = m.planEndTarih,
                planFullDay = m.planFullDay,
                planEkBilgi = m.planEkBilgi,
                planisCompleted = m.planisCompleted,
                planMekan = m.planMekan,
                planStartTarih = m.planStartTarih,
                planUserID = m.planUserID,
                planToKisis = c.ToList()                
            });
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchKisiList()
        {
            //List<KisiSearch> searchListe = db.kisis.Where(m => m.kisiAdi.Contains(searchKelime)).Select(m => new KisiSearch
            //{
            //    kisiID = m.kisiID,
            //    kisiAdi = m.kisiAdi
            //}).ToList();

            List<KisiSearch> liste = db.kisis.Select(m => new KisiSearch {
                kisiID = m.kisiID,
                kisiAdi= m.kisiAdi
            }).ToList();
            return Json (liste, JsonRequestBehavior.AllowGet );
        }

        [HttpPost]
        public JsonResult SaveEvent(plan e)
        {
            var status = false;
            if (e.planID > 0)
            {
                //Update the event
                var v = db.plans.Where(a => a.planID== e.planID).FirstOrDefault();
                if (v != null)
                {
                    v.planKisaBilgi = e.planKisaBilgi;
                    v.planStartTarih = e.planStartTarih;
                    v.planEndTarih = e.planEndTarih;
                    v.planUzunBilgi = e.planUzunBilgi;
                    v.planFullDay = e.planFullDay;
                    v.planColor = e.planColor;
                    v.planMekan = e.planMekan;
                    v.planEkBilgi = e.planEkBilgi;
                    v.planToKisis = e.planToKisis;
                }
            }
            else
            {
                db.plans.Add(e);
            }
            db.SaveChanges();
            status = true;
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int? id)
        {
            var status = false;
            var v = db.plans.Where(a => a.planID == id).FirstOrDefault();
            if (v!=null)
            {
                db.plans.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
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