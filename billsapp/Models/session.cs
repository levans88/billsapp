//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace billsapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class session
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public session()
        {
            this.bills_sessions = new HashSet<bills_sessions>();
            this.sessions_payers = new HashSet<sessions_payers>();
        }
    
        public int session_id { get; set; }
        public System.DateTime session_date { get; set; }
        public decimal session_total_spent { get; set; }
        public string session_status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bills_sessions> bills_sessions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sessions_payers> sessions_payers { get; set; }
    }
}