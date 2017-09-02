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
            ViewBag.yonetimList = new SelectList(db.kisis.Where(m=>m.kisiUnvan.Contains("uzman")), "kisiID","kisiAdi");
            return View();
        }
        
        [HttpPost]
        public JsonResult GetEvents(int? id)
        {
            //var c = db.planToKisis.Select(m=> new getKatilimci {
            //    pkID=m.pkID,
            //    pkKisiID=m.pkKisiID,
            //    pkPlanID=m.pkPlanID,
            //    pkisSource=m.pkisSource,
            //    pkKisiAdi= m.kisi.kisiAdi                
            //});
            var ce = db.planToKisis.Where(m=>m.pkKisiID==id).Select(m => new getKatilimci
            {
                pkID = m.pkID,
                pkKisiID = m.pkKisiID,
                pkPlanID = m.pkPlanID,
                pkisSource = m.pkisSource,
                pkKisiAdi = m.kisi.kisiAdi
            });
            //var a = db.plans.Select(m => new getPlan
            //{
            //    planID = m.planID,
            //    planKisaBilgi = m.planKisaBilgi,
            //    planUzunBilgi = m.planUzunBilgi,
            //    planColor = m.planColor,
            //    planEndTarih = m.planEndTarih,
            //    planFullDay = m.planFullDay,
            //    planEkBilgi = m.planEkBilgi,
            //    planisCompleted = m.planisCompleted,
            //    planMekan = m.planMekan,
            //    planStartTarih = m.planStartTarih,
            //    planUserID = m.planUserID,
            //    planToKisis = c.Where(k=>k.pkPlanID== m.planID).ToList()               
            //});
            kisi kisi = db.kisis.Where(m => m.kisiID == id).SingleOrDefault();            
            List<getPlan> liste = new List<getPlan>();
            getPlan temp = new getPlan();
            var fe = kisi.planToKisis;
            foreach (var item in fe)
            {
                temp.planID = item.plan.planID;
                temp.planKisaBilgi = item.plan.planKisaBilgi;
                temp.planUzunBilgi = item.plan.planUzunBilgi;
                temp.planColor = item.plan.planColor;
                temp.planEndTarih = item.plan.planEndTarih;
                temp.planStartTarih = item.plan.planStartTarih;
                temp.planFullDay = item.plan.planFullDay;
                temp.planEkBilgi = item.plan.planEkBilgi;
                temp.planisCompleted = item.plan.planisCompleted;
                temp.planMekan = item.plan.planMekan;
                temp.planUserID = item.plan.planUserID;
                temp.planToKisis = ce.Where(k=>k.pkPlanID==item.pkPlanID).ToList();                
                liste.Add(temp);
            }   
            return Json(liste, JsonRequestBehavior.AllowGet);
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
                var ptk = db.planToKisis.Where(m => m.pkPlanID == e.planID).ToList();
                
                if (v != null)
                {
                    if (e.planToKisis.Count()==0)
                    {
                        foreach (var item in ptk)
                        {
                            db.planToKisis.Remove(item);
                        }
                    }
                    else
                    {
                        if (e.planToKisis.ToList()[0].pkID == 0)
                        {
                            foreach (var item in ptk)
                            {
                                db.planToKisis.Remove(item);
                            }
                            foreach (var item in e.planToKisis)
                            {
                                db.planToKisis.Add(item);
                            }
                        }
                    }
                    
                    v.planKisaBilgi = e.planKisaBilgi;
                    v.planStartTarih = e.planStartTarih;
                    v.planEndTarih = e.planEndTarih;
                    v.planUzunBilgi = e.planUzunBilgi;
                    v.planFullDay = e.planFullDay;
                    v.planColor = e.planColor;
                    v.planMekan = e.planMekan;
                    v.planEkBilgi = e.planEkBilgi;                    
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