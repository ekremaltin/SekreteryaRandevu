//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SekreteryaRandevu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class planKisi
    {
        public int pkID { get; set; }
        public string pKisiAdi { get; set; }
        public string pKisiMail { get; set; }
        public string pKisiTel { get; set; }
        public Nullable<int> pKisiPlanID { get; set; }
    
        public virtual plan plan { get; set; }
    }
}