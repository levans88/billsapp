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
    
    public partial class payers_permissions
    {
        public int id { get; set; }
        public int source_payer_id { get; set; }
        public int target_payer_id { get; set; }
        public int permission_id { get; set; }
        public int permission_level_id { get; set; }
    
        public virtual payer payer { get; set; }
        public virtual permission permission { get; set; }
        public virtual permission_level permission_level { get; set; }
    }
}
