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
    
    public partial class iletisim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public iletisim()
        {
            this.iletisimToKisis = new HashSet<iletisimToKisi>();
            this.iletisimToSirkets = new HashSet<iletisimToSirket>();
        }
    
        public int iletisimID { get; set; }
        public string iletisimTip { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<iletisimToKisi> iletisimToKisis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<iletisimToSirket> iletisimToSirkets { get; set; }
    }
}