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
    
    public partial class payment_method
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public payment_method()
        {
            this.bill_type = new HashSet<bill_type>();
        }
    
        public int payment_method_id { get; set; }
        public int payer_id { get; set; }
        public string payment_method_name { get; set; }
        public string payment_method_abbreviation { get; set; }
        public int status_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bill_type> bill_type { get; set; }
        public virtual payer payer { get; set; }
        public virtual status status { get; set; }
    }
}