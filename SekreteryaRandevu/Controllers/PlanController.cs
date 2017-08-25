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
            return View();
        }
        
        public JsonResult GetEvents()
        {
            var events = db.plans.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveEvent(plan e)
        {
            var status = false;
            if (e.planID > 0)
            {
                //Update the event
                var v = db.plans.Where(a => a.planID== e.planID).FirstOrDefault();
                //if (v != null)
                //{
                //    v.Subject = e.Subject;
                //    v.Start = e.Start;
                //    v.End = e.End;
                //    v.Description = e.Description;
                //    v.IsFullDay = e.IsFullDay;
                //    v.ThemeColor = e.ThemeColor;
                //}
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