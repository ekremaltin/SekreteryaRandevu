using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SekreteryaRandevu.Models
{
    public class getKatilimci
    {
        public int pkID { get; set; }
        public Nullable<int> pkKisiID { get; set; }
        public Nullable<int> pkPlanID { get; set; }
        public Nullable<bool> pkisSource { get; set; }
    }
}