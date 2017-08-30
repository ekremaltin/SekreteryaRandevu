using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SekreteryaRandevu.Models
{
    public class getPlan
    {
        public int planID { get; set; }
        public string planUzunBilgi { get; set; }
        public string planKisaBilgi { get; set; }
        public Nullable<System.DateTime> planStartTarih { get; set; }
        public Nullable<System.DateTime> planEndTarih { get; set; }
        public string planMekan { get; set; }
        public Nullable<bool> planisCompleted { get; set; }
        public Nullable<bool> planFullDay { get; set; }
        public string planEkBilgi { get; set; }
        public Nullable<int> planUserID { get; set; }
        public string planColor { get; set; }
        public ICollection<getKatilimci> planToKisis { get; set; }
    }
}