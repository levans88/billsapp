using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace billsapp.Enum {
    public enum Permission {
        [Display(Name = "Bill Types")]
        BillTypes = 1,
        [Display(Name = "Payment Methods")]
        PaymentMethods = 2
    }
}